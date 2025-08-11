using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Data;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Entities;

namespace CinemaSolutionApi.Services;

public class AuthService
{
    private readonly CinemaSolutionContext _dbContext;
    private readonly JWT _jwt;

    public AuthService(CinemaSolutionContext dbContext, JWT jwt)
    {
        _dbContext = dbContext;
        _jwt = jwt;
    }

    public async Task<UserResponseDto> CreateUser(CreateUserDto newUser)
    {
        ValidateFields(newUser);
        ValidatePassword(newUser.Password);
        await ValidateEmail(newUser.Email);
        await ValidateUsername(newUser.Username);

        var user = newUser.ToEntity();
        user.Password = PasswordHasher.Hash(newUser.Password);
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user.UserResponseDto();
    }

    public async Task<List<string>> LogIn(LogInDto user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password)) throw new ValidationEx("Username and password are required.");

        var userFound = await FindUsername(user.Username);
        ValidatePassword(user.Password, userFound.Password);

        var token = _jwt.CreateToken(userFound);

        return [token, userFound.Username];
    }

    public void ValidatePassword(string password, string hashedPassword)
    {
        if (!PasswordHasher.Validate(password, hashedPassword)) throw new ValidationEx("Icorrect Password.");
    }

    public async Task<User> FindUsername(string username)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username) ?? throw new ValidationEx("Username not found.");
        return result;
    }
    public void ValidateFields(CreateUserDto NewUser)
    {
        if (string.IsNullOrWhiteSpace(NewUser.Name) || string.IsNullOrWhiteSpace(NewUser.LastName) || string.IsNullOrWhiteSpace(NewUser.Password) || string.IsNullOrWhiteSpace(NewUser.Email)) throw new ValidationEx("Incomplete required information, check that all fields are completed");
    }

    public async Task ValidateEmail(string email)
    {
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!regex.IsMatch(email)) throw new ValidationEx("The email must be like example@domain.com");

        if (await _dbContext.Users.AnyAsync(u => u.Email == email)) throw new ValidationEx($"This email {email} is already registered");
    }

    public async Task ValidateUsername(string username)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Username == username)) throw new ValidationEx($"The username '{username}' is already registered");
    }

    public void ValidatePassword(string password)
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        if (!regex.IsMatch(password)) throw new ValidationEx("The password must have at least 8 characters, one lowercase letter, one uppercase letter, one number and one special character.");
    }
}
