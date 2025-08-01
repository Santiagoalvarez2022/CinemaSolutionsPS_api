namespace CinemaSolutionApi.Dtos;

public record class MovieDto(
    int Id,
    string Name,
    int Duration,
    int IdDirector,
    bool IsInternational
);

