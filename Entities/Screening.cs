using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaSolutionApi.Entities;

public class Screening
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime StartScreening { get; set; }
    public DateTime FinishScreening { get; set; }
    [ForeignKey("MovieId")]
    public Movie? Movie { get; set; }
}