using CinemaSolutionApi.Data;
using CinemaSolutionApi.Entities;
using Microsoft.AspNetCore.Identity;
public class DatabaseSeeder
{
    private readonly CinemaSolutionContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public DatabaseSeeder(CinemaSolutionContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
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
                new Movie {Director=directors[0],Title = "The Secret of the Mirror", Duration = 115, IsInternational = false,  Image = "https://i.ibb.co/S44Sz6z6/Gemini-Generated-Image-wdz3jywdz3jywdz3.png" },
                new Movie {Director=directors[0],Title = "The Secret of the Mirror 2", Duration = 100, IsInternational = false,   Image = "https://i.ibb.co/wN6jnLmf/unnamed-6.png"  },
                new Movie { Director = directors[6], Title = "Nights of Mist", Duration = 90, IsInternational = true, Image = "https://i.ibb.co/VWFHGbGt/Gemini-Generated-Image-6c9kjd6c9kjd6c9k.png" },
                new Movie { Director = directors[8], Title = "The Guardians of the Forest", Duration = 85, IsInternational = true, Image = "https://i.ibb.co/KjQGSPW5/unnamed-1.png" },
                new Movie { Director = directors[9], Title = "Song of Sirens", Duration = 110, IsInternational = false, Image = "https://i.ibb.co/qMnd1w15/unnamed-2.png" },
                new Movie { Director = directors[1], Title = "The Legend of the Awakening", Duration = 155, IsInternational = true, Image = "https://i.ibb.co/60q8DDd4/Gemini-Generated-Image-wdz3jywdz3jywdz3-6.png" },
                new Movie { Director = directors[11], Title = "The Art of Flying", Duration = 125, IsInternational = false, Image = "https://i.ibb.co/WvbBKhfp/unnamed-3.png" },
            };

            await _context.Movies.AddRangeAsync(movies);
            await _context.SaveChangesAsync();
        }
    }
}