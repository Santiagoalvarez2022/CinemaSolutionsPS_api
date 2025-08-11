using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaSolutionApi.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Duration { get; set; }
    public bool IsInternational { get; set; }
    public string Image { get; set; } = "";
    [ForeignKey("DirectorId")]
    public required Director Director { get; set; }
    public List<Screening> Screenings { get; set; } = new List<Screening>();
}