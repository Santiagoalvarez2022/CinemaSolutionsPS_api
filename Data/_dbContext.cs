using CinemaSolutionApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaSolutionApi.Data;

public class CinemaSolutionContext(DbContextOptions<CinemaSolutionContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Screening> Screenings => Set<Screening>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Director>().HasData(
            new { Id = 1, Name = "Luca", LastName = "Stone" },
            new { Id = 2, Name = "Mateus", LastName = "Silver" },
            new { Id = 3, Name = "Pedro", LastName = "Rivera" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new { Id = 1, Title = "The Secret of the Mirror", Duration = 115, IsInternational = false, DirectorId = 1, Image = "https://i.ibb.co/9Hg54sMZ/Gemini-Generated-Image-wdz3jywdz3jywdz3.png" },
            new { Id = 2, Title = "The Forgotten Shadow", Duration = 98, IsInternational = false, DirectorId = 2, Image = "https://i.ibb.co/0pf183VG/Gemini-Generated-Image-wdz3jywdz3jywdz3-1.png" },
            new { Id = 3, Title = "Journey to the Star Heart", Duration = 142, IsInternational = true, DirectorId = 3, Image = "https://i.ibb.co/HDnnNd5t/Gemini-Generated-Image-wdz3jywdz3jywdz3-2.png" },
            new { Id = 4, Title = "The Secret of the Mirror 2", Duration = 115, IsInternational = false, DirectorId = 1, Image = "https://i.ibb.co/8DJdFYwJ/unnamed-6.png" });
    }
}