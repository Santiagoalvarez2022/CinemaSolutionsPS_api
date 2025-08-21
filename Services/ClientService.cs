
using CinemaSolutionApi.Data;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaSolutionApi.Dtos.Ticket;

namespace CinemaSolutionApi.Services;

public class ClientService
{
    private readonly CinemaSolutionContext _dbContext;
    private readonly UserManager<User> _userManager;

    public ClientService(CinemaSolutionContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;

    }

    public async Task CreateTicket(CreateTicketDto newTicket)
    {
        var user = await GetUser(newTicket.IdUser);
        var screening = await GetScreening(newTicket.IdScreening);
        await ValidateLimitTicket(user.Id, screening.Id);
        var ticket = new Ticket() { };
        ticket.User = user;
        ticket.Screening = screening;
        await _dbContext.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetUser(string id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
    }
    public async Task<Screening> GetScreening(int id)
    {
        return await _dbContext.Screenings.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Screening not found");
    }

    public async Task ValidateLimitTicket(string idUser, int idScreening)
    {
        var result = await _dbContext.Tickets
        .Where(t => t.User.Id == idUser && t.Screening.Id == idScreening)
        .CountAsync();

        if (result >= 4) throw new ValidationEx("You can only buy up to 4 tickets per screening.");
    }
}
