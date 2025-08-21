using Microsoft.AspNetCore.Identity;

namespace CinemaSolutionApi.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public List<Ticket> Tickets { get; set; } = new();
}