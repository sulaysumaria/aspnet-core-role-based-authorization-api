# aspnet-core-role-based-authorization-api

ASP.NET Core 2.2 - Role Based Authorization API

## Getting started

```bash
$ dotnet build
$ dotnet run
```

## Topics Covered

- Creating Token
  - Adding Claims (dynamically)
  - Setting Expiry
- Authorizing routes
  - Require all given roles
  - Require any one of the given roles
- Getting details from JWT token

## Routes

| Verb | Path                | Role required |
| ---- | ------------------- | ------------- |
| POST | /users/authenticate |               |
| GET  | /users/             | users:read    |
| GET  | /users/:userId      | users:readme  |

**Note**: `sa` will have access to all routes

## File Structure

- Controllers/UserController.cs - Controller.
  - Routes are defined here.
  - Auth scope required are mentioned here.
- Entities/User.cs
  - User Entity
- Services/UserService.cs
  - Authenticate function to validate user credentials and generate token.
