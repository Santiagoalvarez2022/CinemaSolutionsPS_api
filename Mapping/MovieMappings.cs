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
            Director,
            movie.Director.Id
        );
    }

    public static Movie toEntity(this MovieDto movieDto)
    {
        return new Movie()
        {
            Title = movieDto.Title,
            Duration = movieDto.Duration,
            IsInternational = movieDto.IsInternational,
            Image = movieDto.Image,
        };
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
