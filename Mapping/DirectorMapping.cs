using CinemaSolutionApi.Dtos.Director;
using CinemaSolutionApi.Entities;

namespace CinemaSolutionApi.Mapping;

public static class DirectorMapping
{
    public static DirectorInfoDto ToListInfo(this Director director)
    {
        var fullName = $"{director.Name} {director.LastName}";
        return new(
            director.Id,
            fullName
        );
    }
}