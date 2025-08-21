namespace CinemaSolutionApi.Dtos.Screening;

public record class ScreeningDto(
    int Id,
    decimal Price,
    DateTime StartScreening,
    DateTime FinishScreening,
    int MovieId
);