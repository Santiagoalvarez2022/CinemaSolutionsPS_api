namespace CinemaSolutionApi.Dtos.User;

public record class CreateUserDto(
    string Name,
    string LastName,
    string Password,
    string Username,
    string Email
  );