using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Dtos.Movie;
using CinemaSolutionApi.Dtos.Director;
using CinemaSolutionApi.Data;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Mapping;

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
            .Select(m => m.ToListDto())
            .ToListAsync();
        return movies;
    }
    public async Task<MovieDetailsDto> GetById(int id)
    {
        var movie = await FindMovieById(id);
        return movie.ToDetailsDto();
    }

    public async Task<Movie> FindMovieById(int id)
    {

        var errors = new Dictionary<string, string[]>
        {
            { "Id", new[] { $"Movie not found" } }
        };
        var movie = await _dbContext.Movies
        .AsNoTracking()
        .Where(m => m.Id == id)
        .Include(m => m.Screenings)
        .Include(m => m.Director)
        .AsSplitQuery()
        .FirstOrDefaultAsync() ?? throw new ValidationEx("Movie not found.");

        return movie;
    }

    // public async Task<Movie> FindMovieById(int id)
    // {
    //     var errors = new Dictionary<string, string[]>
    //     {
    //         { "Id", new[] { $"Movie not found" } }
    //     };

    //     return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id) ?? throw new ValidationExProperties(errors);
    // }
    public async Task<MovieDetailsDto> CreateMovie(MovieDto newMovie)
    {
        await ValidateTitle(newMovie.Title, null);
        var director = await FindDirector(newMovie.Director);

        var movie = newMovie.toEntity();
        movie.Director = director;

        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();

        return movie.ToDetailsDto();
    }
    public async Task<MovieDetailsDto> ModifyMovie(int id, MovieDto newMovie)
    {
        await ValidateTitle(newMovie.Title, id);
        var director = await FindDirector(newMovie.Director);

        var movie = await FindMovieById(id);

        movie.Director = director;
        movie.Duration = newMovie.Duration;
        movie.Image = newMovie.Image;
        movie.Title = newMovie.Title;
        movie.IsInternational = newMovie.IsInternational;

        await _dbContext.SaveChangesAsync();
        return movie.ToDetailsDto();
    }

    public async Task ValidateTitle(string title, int? id)
    {
        var errors = new Dictionary<string, string[]>
        {
            { "Title", new[] { $"A movie with the title '{title}' already exists." } }
        };
        if (await _dbContext.Movies.FirstOrDefaultAsync(m => m.Title == title && m.Id != id) != null) throw new ValidationExProperties(errors);
    }
    public async Task<Director> FindDirector(int IdDirector)
    {
        var errors = new Dictionary<string, string[]>
        {
            { "IdDirector", new[] { $"Director not found." } }
        };
        var director = await _dbContext.Directors.FirstOrDefaultAsync(d => d.Id == IdDirector) ?? throw new ValidationExProperties(errors);

        return director;
    }

    public async Task DeleteMovie(int id)
    {
        var errors = new Dictionary<string, string[]>
        {
            { "Id", new[] { $"Movie not found" } }
        };

        var movie = await _dbContext.Movies
        .FirstOrDefaultAsync(m => m.Id == id) ?? throw new ValidationExProperties(errors);

        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();

    }
    public async Task<List<DirectorInfoDto>> GetDirectors()
    {
        return await _dbContext.Directors
            .Select(d => d.ToListInfo())
            .ToListAsync();
    }
};