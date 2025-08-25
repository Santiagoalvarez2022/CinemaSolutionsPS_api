namespace CinemaSolutionApi.Dtos.Screening;

public record class ScreeningDetailsDto(
    int Id,
    decimal Price,
    DateTime StartSceening,
    DateTime FinishScreening,
    int MovieId
);