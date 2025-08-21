using CinemaSolutionApi.Dtos.Screening;
using CinemaSolutionApi.Entities;

namespace CinemaSolutionApi.Mapping;
//
public static class ScreeningMapping
{
    public static Screening ToEntity(this CreateScreeningDto screening)
    {
        return new Screening()
        {
            Price = screening.Price ?? throw new Exception("Price is null"),
            StartScreening = screening.StartScreening,
            FinishScreening = screening.FinishScreening ?? throw new Exception("FinishScreening is null"),
        };
    }

    public static ScreeningDto ToDto(this Screening screening)
    {
        return new(
            screening.Id,
            screening.Price,
            screening.StartScreening,
            screening.FinishScreening,
            screening.Movie!.Id
        );
    }
}
