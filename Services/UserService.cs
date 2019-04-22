using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services {
    public interface IUserService {
        User Authenticate (string username, string password);
        IEnumerable<User> GetAll ();
        User GetById (int id);
    }

    public class UserService : IUserService {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User> {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = new string[] { "sa" } },
            new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = new string[] { "users:read", "users:readme" } }
        };

        private readonly AppSettings _appSettings;

        public UserService (IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
        }

        public User Authenticate (string username, string password) {
            var user = _users.SingleOrDefault (x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_appSettings.Secret);

            List<Claim> subjectClaims = new List<Claim> ();

            subjectClaims.Add (new Claim (ClaimTypes.Name, user.Id.ToString ()));

            for (int i = 0; i < user.Role.Length; i++) {
                subjectClaims.Add (new Claim (ClaimTypes.Role, user.Role[i]));
            }

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (subjectClaims),
                Expires = DateTime.UtcNow.AddDays (7),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            user.Token = tokenHandler.WriteToken (token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll () {
            // return users without passwords
            return _users.Select (x => {
                x.Password = null;
                return x;
            });
        }

        public User GetById (int id) {
            var user = _users.FirstOrDefault (x => x.Id == id);

            // return user without password
            if (user != null)
                user.Password = null;

            return user;
        }
    }
}
