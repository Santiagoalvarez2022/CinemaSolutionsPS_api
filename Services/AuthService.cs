using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Data;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Helpers;
namespace CinemaSolutionApi.Services;

public class AuthService
{
    public bool ValidateFields(CreateUserDto NewUser, out string? errorMsg)
    {
        if (string.IsNullOrWhiteSpace(NewUser.Name) || string.IsNullOrWhiteSpace(NewUser.LastName) || string.IsNullOrWhiteSpace(NewUser.Password) || string.IsNullOrWhiteSpace(NewUser.Email))
        {
            errorMsg = "Incomplete required information, check that all fields are completed";
            return false;
        }
        errorMsg = null;
        return true;
    }

    public async Task<ValidationsResponse> ValidateEmail(string email, CinemaSolutionContext dbContext)
    {
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!regex.IsMatch(email))
        {
            return new ValidationsResponse(false, "The email must be like example@domain.com");
        }
        if (await dbContext.Users.AnyAsync(u => u.Email == email))
        {
            return new ValidationsResponse(false, $"This email {email} is already registered");
        }
        return new ValidationsResponse(true, null);
    }

    public async Task<ValidationsResponse> ValidateUsername(string username, CinemaSolutionContext dbContext)
    {
        if (await dbContext.Users.AnyAsync(u => u.Username == username))
        {
            return new ValidationsResponse(false, $"The username '{username}' is already registered");
        }
        return new ValidationsResponse(true, null);
    }

    public bool ValidatePassword(string password)
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        return regex.IsMatch(password);
    }
}
