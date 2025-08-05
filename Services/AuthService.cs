using CinemaSolutionApi.Dtos.User;
using System.Text.RegularExpressions;

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

    public bool ValidateEmail(string email)
    {
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }

    public bool ValidatePassword(string password)
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        return regex.IsMatch(password);
    }

}
