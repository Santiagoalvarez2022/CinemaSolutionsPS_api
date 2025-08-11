namespace CinemaSolutionApi.Dtos.Movie;

public record class MovieListDto(
    int Id,
    string Title,
    int Duration,
    bool IsInternational,
    string Image,
    string Director
);