using System.ComponentModel.DataAnnotations.Schema;
namespace CinemaSolutionApi.Entities;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("ScreeningId")]
    public Screening Screening { get; set; }
}
