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

    public bool ValidateFields(CreateScreeningDto screening, out string? errorMsg)
    {
        if (screening.Price == null || screening.StartScreening == null || screening.FinishScreening == null || screening.MovieId == null)
        {
            errorMsg = "Incomplete required information, check that all fields are completed";
            return false;
        }

        if (screening.Price <= 0)
        {
            errorMsg = "The price must be positive and greater than 0";
            return false;
        }

        var now = DateTime.Now;
        if (screening.StartScreening < now)
        {
            errorMsg = "This date is not valid because it is in the past, try again";
            return false;
        }

        if (screening.StartScreening == screening.FinishScreening)
        {
            errorMsg = "The startscreening and finishscreening can't be the same";
            return false;
        }

        errorMsg = null;
        return true;
    }
}