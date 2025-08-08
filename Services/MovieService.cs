using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Dtos.Movie;
using CinemaSolutionApi.Data;

namespace CinemaSolutionApi.Services;

public class MovieService
{
    private readonly CinemaSolutionContext _dbContext;
    public MovieService(CinemaSolutionContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<MovieListDto>> GetMovies()
    {
        var movies = await _dbContext.Movies
            .Include(m => m.Director)
            .Select(m => new MovieListDto
            (
                m.Id,
                m.Title,
                m.Duration,
                m.IsInternational,
                m.Image,
                m.Director.Name
            ))
            .ToListAsync();

        return movies;
    }
};