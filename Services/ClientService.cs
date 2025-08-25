
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
    private readonly AdminService _adminService;
    private readonly ScreeningService _screeningService;

    public ClientService(CinemaSolutionContext dbContext, AdminService adminService, ScreeningService screeningService)
    {
        _dbContext = dbContext;
        _adminService = adminService;
        _screeningService = screeningService;
    }

    public async Task<Screening> GetScreening(int id)
    {
        return await _dbContext.Screenings.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Screening not found");
    }

    public async Task CreateTicket(CreateTicketDto newTicket)
    {
        var ticket = new Ticket() { };
        ticket.User = await _adminService.GetUser(newTicket.IdUser);
        ticket.Screening = await _screeningService.GetScreening(newTicket.IdScreening);

        await ValidateLimitTicket(ticket.User.Id, ticket.Screening.Id);

        await _dbContext.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();
    }


    public async Task ValidateLimitTicket(string idUser, int idScreening)
    {
        var result = await _dbContext.Tickets
        .Where(t => t.User.Id == idUser && t.Screening.Id == idScreening)
        .CountAsync();

        if (result >= 4) throw new ValidationEx("You can only buy up to 4 tickets per screening.");
    }
}
