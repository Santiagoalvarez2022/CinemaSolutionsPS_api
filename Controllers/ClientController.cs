using Microsoft.AspNetCore.Authorization;
using CinemaSolutionApi.Services;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using CinemaSolutionApi.Dtos.Ticket;

namespace CinemaSolutionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SysAdmin,Client")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Post(CreateTicketDto newTicket)
        {
            try
            {
                await _clientService.CreateTicket(newTicket);
                return Ok();
            }
            catch (ValidationEx ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
