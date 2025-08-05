using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Dtos.User;

namespace CinemaSolutionApi.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto newUser)
    {
        return new User()
        {
            Name = newUser.Name,
            LastName = newUser.LastName,
            Password = newUser.Password,
            Username = newUser.Username,
            Email = newUser.Email
        };
    }

    public static UserResponseDto ToDto(this User user)
    {
        return new(
            user.Email,
            user.Username
        );
    }

}