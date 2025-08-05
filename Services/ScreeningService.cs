using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Dtos.Screening;

namespace CinemaSolutionApi.Services;

public class ServiceScreening
{
    private readonly List<Screening> _screenings;

    public ServiceScreening(List<Screening> screenings)
    {
        _screenings = screenings;
    }
    public bool ValidateOverlap(CreateScreeningDto screening)
    {
        return _screenings.Any(s => s.StartScreening < screening.FinishScreening && screening.StartScreening < s.FinishScreening);
    }

    public bool ValidateIsInternational(DateTime newScreeningDate)
    {
        var internationalScreeningsToday = _screenings
            .Where(s => s.Movie.IsInternational && s.StartScreening >= newScreeningDate && s.StartScreening < newScreeningDate.AddDays(1))
            .Count();
        return internationalScreeningsToday >= 8;
    }

    public bool ValidateDirectorDailyLimitReached(Movie movie, DateTime newScreeningDate)
    {
        var amountDirectorPerDay = _screenings
            .Where(s => s.Movie?.DirectorId == movie.DirectorId &&
            s.StartScreening >= newScreeningDate &&
            s.StartScreening < newScreeningDate.AddDays(1))
            .Count();
        return amountDirectorPerDay >= 10;
    }

}