namespace CinemaSolutionApi.Dtos.User;

public record class SignUpUserDto(
    string Name,
    string LastName,
    string Password,
    string Username,
    string Email
);