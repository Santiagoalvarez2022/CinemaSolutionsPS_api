using CinemaSolutionApi.Dtos.Screening;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Data;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Mapping;

namespace CinemaSolutionApi.Services;

public class ScreeningService
{
    private readonly CinemaSolutionContext _dbContext;
    public ScreeningService(CinemaSolutionContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> DeleteScreening(int id)
    {
        var screening = await _dbContext.Screenings.FirstOrDefaultAsync(s => s.Id == id);
        if (screening is null) return false;
        _dbContext.Screenings.Remove(screening);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ScreeningDto> AddScreening(CreateScreeningDto newScreening)
    {
        ValidateFields(newScreening);
        await ValidateOverlap(newScreening, null);

        var movie = await MovieExists(newScreening.MovieId!.Value);
        var newScreeningDate = newScreening.StartScreening.Date;
        if (movie.IsInternational)
        {
            await ValidateIsInternational(newScreeningDate, null);
        }
        await ValidateDirectorDailyLimitReached(movie.Director.Id, newScreeningDate, null);

        var screening = newScreening.ToEntity();
        screening.Movie = movie;

        await _dbContext.Screenings.AddAsync(screening);
        await _dbContext.SaveChangesAsync();
        return screening.ToDto();
    }
    public async Task<ScreeningDto> ModifyScreening(int id, CreateScreeningDto newScreening)
    {
        var screening = await _dbContext.Screenings.FirstOrDefaultAsync(s => s.Id == id);
        if (screening == null) throw new ValidationEx("Screening not found.");

        ValidateFields(newScreening);
        await ValidateOverlap(newScreening, id);

        var movie = await MovieExists(newScreening.MovieId!.Value);
        var date = newScreening.StartScreening.Date;

        if (movie.IsInternational)
        {
            await ValidateIsInternational(date, id);
        }

        await ValidateDirectorDailyLimitReached(movie.Director.Id, date, id);

        screening.Price = (decimal)newScreening.Price!;
        screening.StartScreening = newScreening.StartScreening;
        screening.FinishScreening = (DateTime)newScreening.FinishScreening!;
        screening.Movie = movie;

        await _dbContext.SaveChangesAsync();
        return screening.ToDto();
    }

    public async Task<Movie> MovieExists(int id)
    {
        var movie = await _dbContext.Movies
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id) ?? throw new ValidationEx("Movie not found.");
        return movie;
    }
    public async Task ValidateOverlap(CreateScreeningDto screening, int? id)
    {
        var result = await _dbContext.Screenings
            .Where(s => s.Id != id && s.StartScreening.Date == screening.StartScreening.Date)
            .AnyAsync(s => s.StartScreening < screening.FinishScreening && screening.StartScreening < s.FinishScreening);
        if (result) throw new ValidationEx("Sorry, the time slot you've chosen is already taken by another movie");
    }

    public async Task ValidateIsInternational(DateTime newScreeningDate, int? id)
    {
        var internationalScreeningsToday = await _dbContext.Screenings
            .Where(s => s.Id != id && s.Movie!.IsInternational && s.StartScreening >= newScreeningDate && s.StartScreening < newScreeningDate.AddDays(1))
            .CountAsync();
        if (internationalScreeningsToday >= 8) throw new ValidationEx("Sorry, the limit of international film screenings per day has been reached. Try a different date");
    }

    public async Task ValidateDirectorDailyLimitReached(int DirectorId, DateTime newScreeningDate, int? id)
    {
        var amountDirectorPerDay = await _dbContext.Screenings
            .Where(s => s.Id != id && s.Movie!.Director.Id == DirectorId &&
            s.StartScreening >= newScreeningDate &&
            s.StartScreening < newScreeningDate.AddDays(1))
            .CountAsync();
        if (amountDirectorPerDay >= 10) throw new ValidationEx("Sorry, the director of this film reached the limit of daily movie screenings. Try a different date");
    }

    public void ValidateFields(CreateScreeningDto screening)
    {
        if (screening.Price == null || screening.FinishScreening == null || screening.MovieId == null) throw new ValidationEx("Incomplete required information, check that all fields are completed");

        if (screening.Price <= 0) throw new ValidationEx("The price must be positive and greater than 0");

        var now = DateTime.Now;
        if (screening.StartScreening < now) throw new ValidationEx("This date is not valid because it is in the past, try again");

        if (screening.StartScreening == screening.FinishScreening) throw new ValidationEx("The startscreening and finishscreening can't be the same");
    }
}