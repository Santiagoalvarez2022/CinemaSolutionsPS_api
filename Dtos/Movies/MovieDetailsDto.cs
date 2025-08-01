namespace CinemaSolutionApi.Dtos.Movie;

public record class MovieDetailsDto(
    int Id,
    string Title,
    int Duration,
    bool IsInternational,
    string Image,
    int DirectorId,
    string Director,
    ICollection<ScreeningDto> Screenings
);