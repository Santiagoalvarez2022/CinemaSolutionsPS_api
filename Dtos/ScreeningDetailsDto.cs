namespace CinemaSolutionApi.Dtos;

//this class is to send to client.
public record class ScreeningDetailsDto(
    int Id,
    decimal Price,
    DateTime StartSceening,
    DateTime FinishScreening,
    int MovieId
);