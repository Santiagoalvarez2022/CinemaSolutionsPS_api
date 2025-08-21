namespace CinemaSolutionApi.Dtos.Admin;

public record class GetUserDto(
    string Role,
    string Name,
    string LastName,
    string Username,
    string Email,
    string Id
    );