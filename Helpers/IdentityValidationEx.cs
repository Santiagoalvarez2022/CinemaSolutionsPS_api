namespace CinemaSolutionApi.Helpers;

public class IdentityValidationEx : Exception
{
    public Dictionary<string, List<string>>  Errors { get; }

    public IdentityValidationEx (Dictionary<string, List<string>> errors)
    {
        Errors = errors;
    }
}