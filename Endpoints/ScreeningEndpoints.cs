using CinemaSolutionApi.Data;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Mapping;
using CinemaSolutionApi.Services;
using CinemaSolutionApi.Dtos.Screening;
using Microsoft.EntityFrameworkCore;

namespace CinemaSolutionApi.Endpoints;

public static class ScreeningEndpoints
{
    public static RouteGroupBuilder MapScreeningEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/screening");

        group.MapPost("/", async (CreateScreeningDto newScreening, CinemaSolutionContext dbContext) =>
        {
            try
            {
                var screenings = await dbContext.Screenings
                .Include(s => s.Movie)
                .Where(s => s.StartScreening.Date == newScreening.StartScreening.Date)
                .ToListAsync();

                var ServiceScreening = new ServiceScreening(screenings);

                if (!ServiceScreening.ValidateFields(newScreening, out string? errorMsg)) return Results.BadRequest(errorMsg);

                if (ServiceScreening.ValidateOverlap(newScreening)) return Results.BadRequest("Sorry, the time slot you've chosen is already taken by another movie");

                var movie = await dbContext.Movies.FindAsync(newScreening.MovieId);

                if (movie == null) return Results.BadRequest("Movie not found");

                var newScreeningDate = newScreening.StartScreening.Date;
                if (movie.IsInternational && ServiceScreening.ValidateIsInternational(newScreeningDate))
                {
                    return Results.BadRequest("Sorry, the limit of international film screenings per day has been reached. Try a different date");
                }

                if (ServiceScreening.ValidateDirectorDailyLimitReached(movie, newScreeningDate))
                {
                    return Results.BadRequest("Sorry, the director of this film reached the limit of daily movie screenings. Try a different date");
                }

                Screening screening = newScreening.ToEntity();
                screening.Movie = await dbContext.Movies.FindAsync(newScreening.MovieId);
                dbContext.Screenings.Add(screening);
                dbContext.SaveChanges();
                return Results.Ok(screening.ToDto());
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Unexpected error: " + ex.Message);
            }
        }).RequireAuthorization();

        group.MapPut("/{id:int}", async (int id, CreateScreeningDto updatedScreening, CinemaSolutionContext dbContext) =>
        {
            try
            {
                var ScreeningFound = await dbContext.Screenings.FindAsync(id);
                if (ScreeningFound == null) return Results.NotFound("Screening not found");

                var screenings = await dbContext.Screenings
                .Include(s => s.Movie)
                .Where(s => s.Id != id && s.StartScreening.Date == updatedScreening.StartScreening.Date)
                .ToListAsync();
                var ServiceScreening = new ServiceScreening(screenings);

                if (!ServiceScreening.ValidateFields(updatedScreening, out string? errorMsg)) return Results.BadRequest(errorMsg);

                if (ServiceScreening.ValidateOverlap(updatedScreening)) return Results.BadRequest("Sorry, the time slot you've chosen is already taken by another movie");

                var movie = await dbContext.Movies.FindAsync(updatedScreening.MovieId);

                if (movie == null) return Results.BadRequest("Movie not found");

                var newScreeningDate = updatedScreening.StartScreening.Date;
                if (movie.IsInternational && ServiceScreening.ValidateIsInternational(newScreeningDate))
                {
                    return Results.BadRequest("Sorry, the limit of international film screenings per day has been reached. Try a different date");
                }

                if (ServiceScreening.ValidateDirectorDailyLimitReached(movie, newScreeningDate))
                {
                    return Results.BadRequest("Sorry, the director of this film reached the limit of daily movie screenings. Try a different date");
                }

                ScreeningFound.Price = (decimal)updatedScreening.Price!;
                ScreeningFound.StartScreening = updatedScreening.StartScreening;
                ScreeningFound.FinishScreening = (DateTime)updatedScreening.FinishScreening!;
                ScreeningFound.MovieId = (int)updatedScreening.MovieId!;
                ScreeningFound.Movie = movie;

                await dbContext.SaveChangesAsync();
                return Results.Created("", ScreeningFound.ToDto());

            }
            catch (Exception ex)
            {
                return Results.BadRequest("Unexpected error: " + ex.Message);
            }
        }).RequireAuthorization();

        group.MapDelete("/{id:int}", async (int id, CinemaSolutionContext dbContext) =>
       {
           try
           {
               var screening = await dbContext.Screenings.FindAsync(id);
               if (screening == null) return Results.NotFound();

               dbContext.Screenings.Remove(screening);
               await dbContext.SaveChangesAsync();

               return Results.Ok();
           }
           catch (Exception ex)
           {
               return Results.BadRequest("Unexpected error: " + ex.Message);
           }
       }).RequireAuthorization();

        return group;
    }
}