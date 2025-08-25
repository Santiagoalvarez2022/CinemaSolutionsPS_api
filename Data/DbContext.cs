using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CinemaSolutionApi.Entities;

namespace CinemaSolutionApi.Data;

public class CinemaSolutionContext : IdentityDbContext
{
    public CinemaSolutionContext(DbContextOptions<CinemaSolutionContext> options) : base(options) { }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Screening> Screenings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
        .HasIndex(m => m.Title)
        .IsUnique();
    }
}
