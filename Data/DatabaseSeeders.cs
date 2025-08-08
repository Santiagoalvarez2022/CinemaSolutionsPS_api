using CinemaSolutionApi.Data;
using CinemaSolutionApi.Entities;
public class DatabaseSeeder
{
    private readonly CinemaSolutionContext _context;

    public DatabaseSeeder(CinemaSolutionContext context)
    {
        _context = context;
    }


    public async Task SeedAsync()
    {
        if (!_context.Directors.Any())
        {
            var directors = new List<Director>
            {
                new Director {  Name = "Luca", LastName = "Stone" },
                new Director { Name = "Mateus", LastName = "Silver" },
                new Director { Name = "Pedro", LastName = "Rivera" },
                new Director { Name = "Liam", LastName = "O'Connell" },
                new Director { Name = "Olivia", LastName = "Parker" },
                new Director { Name = "Noah", LastName = "Harris" },
                new Director { Name = "Emma", LastName = "Collins" },
                new Director { Name = "William", LastName = "Reed" },
                new Director { Name = "Sophia", LastName = "Bennett" },
                new Director { Name = "James", LastName = "Foster" },
                new Director { Name = "Isabella", LastName = "Morgan" },
                new Director { Name = "Benjamin", LastName = "Hayes" },
                new Director { Name = "Ava", LastName = "Sullivan" },
                new Director { Name = "Ethan", LastName = "Brooks" }
            };

            await _context.Directors.AddRangeAsync(directors);
            await _context.SaveChangesAsync();

            var movies = new List<Movie>
            {
                new Movie {Director=directors[0],Title = "The Secret of the Mirror", Duration = 115, IsInternational = false,  Image = "https://i.ibb.co/9Hg54sMZ/Gemini-Generated-Image-wdz3jywdz3jywdz3.png" },
                new Movie {Director=directors[2],Title = "The Forgotten Shadow", Duration = 98, IsInternational = false,    Image = "https://i.ibb.co/0pf183VG/Gemini-Generated-Image-wdz3jywdz3jywdz3-1.png" },
                new Movie {Director=directors[3],Title = "Journey to the Star Heart", Duration = 102, IsInternational = true,   Image = "https://i.ibb.co/HDnnNd5t/Gemini-Generated-Image-wdz3jywdz3jywdz3-2.png" },
                new Movie {Director=directors[0],Title = "The Secret of the Mirror 2", Duration = 100, IsInternational = false,   Image = "https://i.ibb.co/8DJdFYwJ/unnamed-6.png" },
                new Movie {Director=directors[4],Title = "Chronicles of the Hidden City", Duration = 105, IsInternational = false,    Image = "https://i.ibb.co/MrkwZZz/Gemini-Generated-Image-wdz3jywdz3jywdz3-3.png" },
                new Movie {Director=directors[5],Title = "The Dragon's Last Breath", Duration = 60, IsInternational = true,    Image = "https://i.ibb.co/nMZV6ZC6/Gemini-Generated-Image-wdz3jywdz3jywdz3-4.png" },
                new Movie {Director=directors[6],Title = "Nights of Mist", Duration = 90, IsInternational = true,    Image = "https://i.ibb.co/3YjvSLQX/unnamed.png" },
                new Movie {Director=directors[7],Title = "The Enigma of the Hourglass", Duration = 130, IsInternational = false,    Image = "https://i.ibb.co/4n92fjpr/Gemini-Generated-Image-wdz3jywdz3jywdz3-5.png" },
                new Movie {Director=directors[8],Title = "The Guardians of the Forest", Duration = 85, IsInternational = true,    Image = "https://i.ibb.co/9HVvNRY8/unnamed-1.png" },
                new Movie {Director=directors[9],Title = "Song of Sirens", Duration = 110, IsInternational = false,    Image = "https://i.ibb.co/My8bMQyf/unnamed-2.png" },
                new Movie {Director=directors[1],Title = "The Legend of the Awakening", Duration = 155, IsInternational = true,    Image = "https://i.ibb.co/901bbwy/Gemini-Generated-Image-wdz3jywdz3jywdz3-7.png" },
                new Movie {Director=directors[10],Title = "Echoes in the Void", Duration = 100, IsInternational = true,    Image = "https://i.ibb.co/vvd09nkH/Gemini-Generated-Image-6c9kjd6c9kjd6c9k-1.png" },
                new Movie {Director=directors[11],Title = "The Art of Flying", Duration = 125, IsInternational = false,    Image = "https://i.ibb.co/bgV5f4Hh/unnamed-3.png" },
                new Movie {Director=directors[12],Title = "Whispers of the Past", Duration = 95, IsInternational = true,    Image = "https://i.ibb.co/YT1d7NB2/Gemini-Generated-Image-6c9kjd6c9kjd6c9k-2.png" },
                new Movie {Director=directors[13],Title = "The Dance of the Fireflies", Duration = 108, IsInternational = true,   Image = "https://i.ibb.co/8nqJHMbm/unnamed-4.png" }
            };

            await _context.Movies.AddRangeAsync(movies);
            await _context.SaveChangesAsync();
        }
    }
}