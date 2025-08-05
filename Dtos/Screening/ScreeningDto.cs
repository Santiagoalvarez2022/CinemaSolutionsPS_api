namespace CinemaSolutionApi.Dtos.Screening;

//this class is to send to client.
public record class ScreeningDto(
    int Id,
    decimal Price,
    DateTime StartScreening,
    DateTime FinishScreening,
    int MovieId
);