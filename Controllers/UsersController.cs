using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers {

    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class UsersController : ControllerBase {
        private IUserService _userService;

        public UsersController (IUserService userService) {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost ("authenticate")]
        public IActionResult Authenticate ([FromBody] User userParam) {
            var user = _userService.Authenticate (userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest (new { message = "Username or password is incorrect" });

            return Ok (user);
        }

        // This means any one of Admin or sa
        [Authorize (Roles = "users:read,sa")]
        // This means both Admin and sa
        // [Authorize(Roles = "Admin")]
        // [Authorize(Roles = "sa")]
        [HttpGet]
        public IActionResult GetAll () {
            var users = _userService.GetAll ();

            return Ok (users);
        }

        [Authorize (Roles = "users:readme,sa")]
        [HttpGet ("{id}")]
        public IActionResult GetById (int id) {
            var user = _userService.GetById (id);

            if (user == null) {
                return NotFound ();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse (User.Identity.Name);

            if (id != currentUserId && !User.IsInRole ("Admin")) {
                return Forbid ();
            }

            return Ok (user);
        }
    }
}
