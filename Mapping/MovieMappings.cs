using CinemaSolutionApi.Dtos.Movie;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Dtos.Screening;
namespace CinemaSolutionApi.Mapping;

public static class MovieMapping
{
    public static MovieListDto ToListDto(this Movie movie)
    {
        var Director = movie.Director.Name + " " + movie.Director.LastName;
        return new(
            movie.Id,
            movie.Title,
            movie.Duration,
            movie.IsInternational,
            movie.Image,
            Director
        );
    }
    public static MovieDetailsDto ToDetailsDto(this Movie movie)
    {
        var Director = movie.Director.Name + " " + movie.Director.LastName;
        return new(
            movie.Id,
            movie.Title,
            movie.Duration,
            movie.IsInternational,
            movie.Image,
            movie.Director.Id,
            Director,
            [.. movie.Screenings.Select(s => s.ToDto())]
        );
    }
}
