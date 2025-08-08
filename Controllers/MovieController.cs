using CinemaSolutionApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSolutionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _movieService.GetMovies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    };
}