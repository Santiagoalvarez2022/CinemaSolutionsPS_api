namespace CinemaSolutionApi.Entities;

public class Director
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public List<Movie> Movies { get; set; } = new List<Movie>();
}