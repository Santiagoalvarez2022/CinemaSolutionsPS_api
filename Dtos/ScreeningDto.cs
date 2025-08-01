namespace CinemaSolutionApi.Dtos;

//this class is to send to client.
public record class ScreeningDto(
    int Id,
    decimal Price,
    DateTime StartSceening,
    DateTime FinishScreening,
    int MovieId
);