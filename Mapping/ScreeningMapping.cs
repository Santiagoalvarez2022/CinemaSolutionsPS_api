using CinemaSolutionApi.Dtos;
using CinemaSolutionApi.Entities;

namespace CinemaSolutionApi.Mapping;
//
public static class ScreeningMapping
{
    public static Screening ToEntity(this CreateScreeningDto screening)
    {
        return new Screening()
        {
            Price = screening.Price,
            StartScreening = screening.StartSceening,
            FinishScreening = screening.FinishScreening,
            MovieId = screening.MovieId,
        };
    }

    public static ScreeningDto ToDto(this Screening screening)
    {
        return new(
            screening.Id,
            screening.Price,
            screening.StartScreening,
            screening.FinishScreening,
            screening.MovieId
        );
    }
}
