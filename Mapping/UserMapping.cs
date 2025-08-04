using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Dtos.User;

namespace CinemaSolutionApi.Mapping;

public static class UserMapping
{
    //toma como parametro la http request y devuelve una intancia
    //de la entidad
    public static User ToEntity(this CreateUserDto newUser)
    {
        //creo una instancia de CreateUserDto
        //que es similar ala estrucutura de la entidad User

        //hash password.
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