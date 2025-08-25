using System.ComponentModel.DataAnnotations.Schema;
namespace CinemaSolutionApi.Entities;

public class Ticket
{
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("ScreeningId")]
    public Screening Screening { get; set; }
}
