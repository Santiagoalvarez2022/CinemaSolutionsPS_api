using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Data;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Identity;


namespace CinemaSolutionApi.Services;

public class AuthService
{
    private readonly CinemaSolutionContext _dbContext;
    private readonly JWT _jwt;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AuthService(CinemaSolutionContext dbContext, JWT jwt, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _dbContext = dbContext;
        _jwt = jwt;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserResponseDto> CreateUser(SignUpUserDto newUser)
    {
        ValidateFields(newUser);
        await ValidateEmail(newUser.Email);
        await ValidateUsername(newUser.Username);

        var user = newUser.ToEntity();
        var result = await _userManager.CreateAsync(user, newUser.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return user.UserResponseDto();
    }

    public async Task<List<string>> LogIn(LogInDto logInData)
    {
        if (string.IsNullOrWhiteSpace(logInData.Username) || string.IsNullOrWhiteSpace(logInData.Password)) throw new ValidationEx("Username and password are required.");

        var user = await _userManager.FindByNameAsync(logInData.Username);
        if (user == null) throw new ValidationEx("User not found con identitys");

        var result = await _signInManager.CheckPasswordSignInAsync(user, logInData.Password, false);

        if (!result.Succeeded) throw new ValidationEx("Incorrect password");

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwt.CreateToken(user, roles);

        return [token, user.UserName];
    }

    public void ValidatePassword(string password, string hashedPassword)
    {
        if (!PasswordHasher.Validate(password, hashedPassword)) throw new ValidationEx("Icorrect Password.");
    }

    public async Task<User> FindUsername(string username)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username) ?? throw new ValidationEx("Username not found.");
        return result;
    }
    public void ValidateFields(SignUpUserDto NewUser)
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
        if (await _dbContext.Users.AnyAsync(u => u.UserName == username)) throw new ValidationEx($"The username '{username}' is already registered");
    }

    public void ValidatePassword(string password)
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        if (!regex.IsMatch(password)) throw new ValidationEx("The password must have at least 8 characters, one lowercase letter, one uppercase letter, one number and one special character.");
    }
}