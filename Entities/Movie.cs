namespace CinemaSolutionApi.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Duration { get; set; }
    public bool IsInternational { get; set; }
    public string Image { get; set; } = "";
    public int DirectorId { get; set; }
    public required Director Director { get; set; }
    public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}