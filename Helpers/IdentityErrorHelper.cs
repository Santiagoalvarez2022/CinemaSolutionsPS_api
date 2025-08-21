using Microsoft.AspNetCore.Identity;
namespace CinemaSolutionApi.Helpers;

public static class IdentityErrorHelper
{

    public static Dictionary<string, List<string>> FormatErrors(IEnumerable<IdentityError> errors)
    {
        var error = new Dictionary<string, List<string>>();

        foreach (var e in errors)
        {
            var field = e.Code switch
            {
                var code when code.Contains("DuplicateUserName") => "Username",
                var code when code.Contains("DuplicateEmail") => "Email",
                var code when code.Contains("Password") => "Password",
                _ => "Other"
            };

            if (!error.ContainsKey(field))
                error[field] = new List<string>();

            error[field].Add(e.Description);
        }
        return error;
    }
}