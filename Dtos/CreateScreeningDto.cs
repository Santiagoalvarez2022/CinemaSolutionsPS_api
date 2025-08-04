namespace CinemaSolutionApi.Dtos;

//CreateScreeningDto a diferencia de Screening Dto, no tiene el Id
//
public record class CreateScreeningDto(
    decimal Price,
    DateTime StartSceening,
    DateTime FinishScreening,
    int MovieId
    );
