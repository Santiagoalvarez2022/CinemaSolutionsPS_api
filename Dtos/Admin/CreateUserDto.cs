using System.ComponentModel.DataAnnotations;

namespace CinemaSolutionApi.Dtos.Admin;

public record class CreateUserDto(
    [Required(ErrorMessage = "Role can't be empty")]
    string Role,

    [Required(ErrorMessage = "Name can't be empty")]
    string Name,

    [Required(ErrorMessage = "Last name can't be empty")]
    string LastName,

    [Required(ErrorMessage = "Password can't be empty")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$",
    ErrorMessage = "Password must contain upper, lower, number, and special character")]
    string Password,

    [Required(ErrorMessage = "Username can't be empty")]
    string Username,

    [Required(ErrorMessage = "Email can't be empty")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
    string Email

    );