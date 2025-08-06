using CinemaSolutionApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaSolutionApi.Data;

public class CinemaSolutionContext(DbContextOptions<CinemaSolutionContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Screening> Screenings => Set<Screening>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Director>().HasData(
            new { Id = 1, Name = "Luca", LastName = "Stone" },
            new { Id = 2, Name = "Mateus", LastName = "Silver" },
            new { Id = 3, Name = "Pedro", LastName = "Rivera" },
            new { Id = 4, Name = "Liam", LastName = "O'Connell" },
            new { Id = 5, Name = "Olivia", LastName = "Parker" },
            new { Id = 6, Name = "Noah", LastName = "Harris" },
            new { Id = 7, Name = "Emma", LastName = "Collins" },
            new { Id = 8, Name = "William", LastName = "Reed" },
            new { Id = 9, Name = "Sophia", LastName = "Bennett" },
            new { Id = 10, Name = "James", LastName = "Foster" },
            new { Id = 11, Name = "Isabella", LastName = "Morgan" },
            new { Id = 12, Name = "Benjamin", LastName = "Hayes" },
            new { Id = 13, Name = "Ava", LastName = "Sullivan" },
            new { Id = 14, Name = "Ethan", LastName = "Brooks" }
        );
        modelBuilder.Entity<Movie>().HasData(
            new { Id = 1, Title = "The Secret of the Mirror", Duration = 115, IsInternational = false, DirectorId = 1, Image = "https://i.ibb.co/9Hg54sMZ/Gemini-Generated-Image-wdz3jywdz3jywdz3.png" },
            new { Id = 2, Title = "The Forgotten Shadow", Duration = 98, IsInternational = false, DirectorId = 2, Image = "https://i.ibb.co/0pf183VG/Gemini-Generated-Image-wdz3jywdz3jywdz3-1.png" },
            new { Id = 3, Title = "Journey to the Star Heart", Duration = 102, IsInternational = true, DirectorId = 3, Image = "https://i.ibb.co/HDnnNd5t/Gemini-Generated-Image-wdz3jywdz3jywdz3-2.png" },
            new { Id = 4, Title = "The Secret of the Mirror 2", Duration = 100, IsInternational = false, DirectorId = 1, Image = "https://i.ibb.co/8DJdFYwJ/unnamed-6.png" },
            new { Id = 5, Title = "Chronicles of the Hidden City", Duration = 105, IsInternational = false, DirectorId = 4, Image = "https://i.ibb.co/MrkwZZz/Gemini-Generated-Image-wdz3jywdz3jywdz3-3.png" },
            new { Id = 6, Title = "The Dragon's Last Breath", Duration = 60, IsInternational = true, DirectorId = 5, Image = "https://i.ibb.co/nMZV6ZC6/Gemini-Generated-Image-wdz3jywdz3jywdz3-4.png" },
            new { Id = 7, Title = "Nights of Mist", Duration = 90, IsInternational = true, DirectorId = 6, Image = "https://i.ibb.co/3YjvSLQX/unnamed.png" },
            new { Id = 8, Title = "The Enigma of the Hourglass", Duration = 130, IsInternational = false, DirectorId = 7, Image = "https://i.ibb.co/4n92fjpr/Gemini-Generated-Image-wdz3jywdz3jywdz3-5.png" },
            new { Id = 9, Title = "The Guardians of the Forest", Duration = 85, IsInternational = true, DirectorId = 8, Image = "https://i.ibb.co/9HVvNRY8/unnamed-1.png" },
            new { Id = 10, Title = "Song of Sirens", Duration = 110, IsInternational = false, DirectorId = 9, Image = "https://i.ibb.co/My8bMQyf/unnamed-2.png" },
            new { Id = 11, Title = "The Legend of the Awakening", Duration = 155, IsInternational = true, DirectorId = 10, Image = "https://i.ibb.co/901bbwy/Gemini-Generated-Image-wdz3jywdz3jywdz3-7.png" },
            new { Id = 12, Title = "Echoes in the Void", Duration = 100, IsInternational = true, DirectorId = 11, Image = "https://i.ibb.co/vvd09nkH/Gemini-Generated-Image-6c9kjd6c9kjd6c9k-1.png" },
            new { Id = 13, Title = "The Art of Flying", Duration = 125, IsInternational = false, DirectorId = 12, Image = "https://i.ibb.co/bgV5f4Hh/unnamed-3.png" },
            new { Id = 14, Title = "Whispers of the Past", Duration = 95, IsInternational = true, DirectorId = 13, Image = "https://i.ibb.co/YT1d7NB2/Gemini-Generated-Image-6c9kjd6c9kjd6c9k-2.png" },
            new { Id = 15, Title = "The Dance of the Fireflies", Duration = 108, IsInternational = true, DirectorId = 14, Image = "https://i.ibb.co/8nqJHMbm/unnamed-4.png" });


        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    var converter = new ValueConverter<DateTime, DateTime>(
                        v => v,
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                    property.SetValueConverter(converter);
                }
            }
        }
    }
}
