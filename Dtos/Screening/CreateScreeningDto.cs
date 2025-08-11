namespace CinemaSolutionApi.Dtos.Screening;

public record class CreateScreeningDto(
    decimal? Price,
    DateTime StartScreening,
    DateTime? FinishScreening,
    int? MovieId
    );
