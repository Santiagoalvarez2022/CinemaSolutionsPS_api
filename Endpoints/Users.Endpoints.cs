using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Data;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaSolutionApi.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/users");

        group.MapPost("/register", async (CreateUserDto newUser, CinemaSolutionContext dbContext) =>
        {
            var AuthService = new AuthService();
            if (!AuthService.ValidateFields(newUser, out string? errorMsg))
            {
                return Results.BadRequest(errorMsg);
            }
            var emailValidate = await AuthService.ValidateEmail(newUser.Email, dbContext);
            if (!emailValidate.IsValid)
            {
                return Results.Conflict(emailValidate.Message);
            }

            var usernameValidate = await AuthService.ValidateUsername(newUser.Username, dbContext);
            if (!usernameValidate.IsValid)
            {
                return Results.Conflict(usernameValidate.Message);
            }

            if (!AuthService.ValidatePassword(newUser.Password))
            {
                return Results.BadRequest("The password must have at least 8 characters, one lowercase letter, one uppercase letter, one number and one special character.");
            }

            var user = newUser.ToEntity();
            user.Password = PasswordHasher.Hash(newUser.Password);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Results.Created("", user.UserResponseDto());
        });

        group.MapPost("/login", async (LogInDto user, CinemaSolutionContext dbContext, JWT Jwt) =>
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return Results.BadRequest("Username and password are required.");
            }
            var FindUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (FindUser == null) return Results.NotFound("Username not found.");

            if (!PasswordHasher.Validate(user.Password, FindUser.Password)) return Results.BadRequest("Icorrect Password");

            var token = Jwt.CreateToken(FindUser);

            return Results.Ok(new
            {
                tkn_cinema = token,
                username = user.Username,
            });
        });

        return group;
    }
};