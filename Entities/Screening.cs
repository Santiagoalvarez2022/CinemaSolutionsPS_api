namespace CinemaSolutionApi.Entities;

public class Screening
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime StartScreening { get; set; }
    public DateTime FinishScreening { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
}