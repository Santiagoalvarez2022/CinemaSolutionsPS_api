using CinemaSolutionApi.Data;
using CinemaSolutionApi.Entities;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Mapping;

namespace CinemaSolutionApi.Endpoints;

public static class MoviesEndpoints
{
    public static RouteGroupBuilder MapMoviesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/movies");

        group.MapGet("/", async (CinemaSolutionContext dbContext) =>
        {
            var movies = await dbContext.Movies
                .Include(m => m.Director)
                .ToListAsync();
            return Results.Ok(movies.Select(m => m.ToListDto())); // método de mapeo ToDetailsDto

        });

        group.MapGet("/{id}", async (int id, CinemaSolutionContext dbContext) =>
        {
            var movie = await dbContext.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Screenings)
                .Include(m => m.Director)
                .FirstOrDefaultAsync();
            if (movie is null)
            {
                return Results.NotFound(); // Si no se encuentra la película
            }
            return Results.Ok(movie.ToDetailsDto()); // método de mapeo ToDetailsDto
        });

        return group;
    }

};