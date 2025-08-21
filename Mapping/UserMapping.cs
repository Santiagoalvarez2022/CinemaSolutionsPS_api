using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Dtos.Admin;
using CinemaSolutionApi.Dtos.Ticket;

namespace CinemaSolutionApi.Mapping;

public static class UserMapping
{
    public static User ToEntity(this SignUpUserDto newUser)
    {
        return new User()
        {
            Name = newUser.Name,
            LastName = newUser.LastName,
            UserName = newUser.Username,
            Email = newUser.Email
        };
    }

    public static GetUserDto ToGetUser(this User user, IList<string> roles)
    {
        return new(
            roles[0],
            user.Name,
            user.LastName,
            user.UserName!,
            user.Email!,
            user.Id
        );
    }


    public static UserResponseDto UserResponseDto(this User user)
    {
        return new(
            user.UserName
        );
    }


}