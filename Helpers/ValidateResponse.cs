namespace CinemaSolutionApi.Helpers;

public record ValidationsResponse(
    bool IsValid,
    string? Message
);