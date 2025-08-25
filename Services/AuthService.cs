using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Data;
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
        var user = newUser.ToEntity();
        var result = await _userManager.CreateAsync(user, newUser.Password);

        if (!result.Succeeded)
        {
            var formattedErrors = IdentityErrorHelper.FormatErrors(result.Errors);
            throw new IdentityValidationEx(formattedErrors);
        }

        await _userManager.AddToRoleAsync(user, "Client");

        return user.UserResponseDto();
    }

    public async Task<LogInResponseDto> LogIn(LogInDto logInData)
    {
        if (string.IsNullOrWhiteSpace(logInData.Username) || string.IsNullOrWhiteSpace(logInData.Password)) throw new ValidationEx("Username and password are required.");

        var user = await _userManager.FindByNameAsync(logInData.Username);
        if (user == null) throw new ValidationEx("User not found");

        var result = await _signInManager.CheckPasswordSignInAsync(user, logInData.Password, false);

        if (!result.Succeeded) throw new ValidationEx("Incorrect password");

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwt.CreateToken(user, roles);

        return new LogInResponseDto(token);
    }
}