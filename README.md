# aspnet-core-role-based-authorization-api

ASP.NET Core 2.2 - Role Based Authorization API

## Description

Demonstrating role based authentication API using .NET Core.

## Getting started

```bash
$ dotnet build
$ dotnet run
```

## Topics Covered

- Creating Token
  - <a href="https://github.com/sulaysumaria/aspnet-core-role-based-authorization-api/blob/master/Services/UserService.cs#L47" target="_blank" >Adding Claims (dynamically)</a>
  - <a href="https://github.com/sulaysumaria/aspnet-core-role-based-authorization-api/blob/master/Services/UserService.cs#L53" target="_blank" >Setting Expiry</a>
- Authorizing routes
  - <a href="https://github.com/sulaysumaria/aspnet-core-role-based-authorization-api/blob/master/Controllers/UsersController.cs#L32" target="_blank" >Require all given roles</a>
  - <a href="https://github.com/sulaysumaria/aspnet-core-role-based-authorization-api/blob/master/Controllers/UsersController.cs#L30" target="_blank" >Require any one of the given roles</a>
  - <a href="https://github.com/sulaysumaria/aspnet-core-role-based-authorization-api/blob/master/Controllers/UsersController.cs#L18" target="_blank" >Allowing anonymous access to routes</a>
- Getting details from JWT token
  - <a href="https://github.com/sulaysumaria/aspnet-core-role-based-authorization-api/blob/master/Controllers/UsersController.cs#L51" target="_blank" >Getting userId</a>

## Routes

| Verb | Path                | Role required |
| ---- | ------------------- | ------------- |
| POST | /users/authenticate |               |
| GET  | /users/             | users:read    |
| GET  | /users/:userId      | users:readme  |

**Note**: `sa` will have access to all routes

## File Structure

- `Controllers/UserController.cs`
  - Routes are defined here.
  - Auth scope required are mentioned here.
- `Entities/User.cs`
  - User Entity
- `Services/UserService.cs`
  - Authenticate function to validate user credentials and generate token.
