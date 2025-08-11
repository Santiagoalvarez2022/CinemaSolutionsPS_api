namespace CinemaSolutionApi.Helpers;

public class ValidationEx : Exception
{
    public ValidationEx(string? message) : base(message)
    { }
}