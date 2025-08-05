using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Data;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Helpers;

namespace CinemaSolutionApi.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/users");

        group.MapPost("/register", (CreateUserDto newUser, CinemaSolutionContext dbContext) =>
        {

            var user = newUser.ToEntity();
            user.Password = PasswordHasher.Hash(newUser.Password);
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return Results.Ok(user.ToDto());
        });

        group.MapPost("/login", (LogInDto user, CinemaSolutionContext dbContext, JWT Jwt) =>
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return Results.BadRequest("Username and password are required.");
            }
            var FindUser = dbContext.Users.FirstOrDefault(u => u.Username == user.Username);

            if (FindUser is null)
            {
                return Results.BadRequest("User not found.");
            }

            if (!PasswordHasher.Validate(user.Password, FindUser.Password))
            {
                return Results.BadRequest("Icorrect Password");

            }

            var token = Jwt.CreateToken(FindUser);

            return Results.Ok(new
            {
                token,
                username = user.Username,
            });
        });
        return group;
    }
};