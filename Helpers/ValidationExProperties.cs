namespace CinemaSolutionApi.Helpers;

public class ValidationExProperties : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationExProperties(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}
