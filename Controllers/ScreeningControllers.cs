using CinemaSolutionApi.Dtos.Screening;
using CinemaSolutionApi.Services;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemaSolutionApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : ControllerBase
    {
        private readonly ScreeningService _screeningService;
        public ScreeningController(ScreeningService screeningService)
        {
            _screeningService = screeningService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _screeningService.DeleteScreening(id);
            if (!result) return NotFound();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateScreeningDto screening)
        {
            try
            {
                var result = await _screeningService.AddScreening(screening);
                return Ok(result);
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CreateScreeningDto screening)
        {
            try
            {
                var result = await _screeningService.ModifyScreening(id, screening);
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