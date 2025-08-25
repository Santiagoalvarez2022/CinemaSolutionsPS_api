using CinemaSolutionApi.Services;
using Microsoft.AspNetCore.Mvc;
using CinemaSolutionApi.Helpers;
using CinemaSolutionApi.Dtos.Movie;
using Microsoft.AspNetCore.Authorization;

namespace CinemaSolutionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SysAdmin,CinemaAdmin")]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [AllowAnonymous]
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
        [HttpPost]
        public async Task<IActionResult> Post(MovieDto newMovie)
        {
            try
            {
                return Ok(await _movieService.CreateMovie(newMovie));
            }
            catch (ValidationEx ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationExProperties vex)
            {
                return BadRequest(new
                {
                    errors = vex.Errors
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _movieService.GetById(id);
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
        public async Task<IActionResult> Put(int id, MovieDto movie)
        {
            try
            {
                return Ok(await _movieService.ModifyMovie(id, movie));
            }
            catch (ValidationEx ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationExProperties vex)
            {
                return BadRequest(new
                {
                    errors = vex.Errors
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("directors")]
        public async Task<IActionResult> GetDirectors()
        {
            try
            {
                return Ok(await _movieService.GetDirectors());
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieService.DeleteMovie(id);
                return Ok();
            }
            catch (ValidationEx ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationExProperties vex)
            {
                return BadRequest(new
                {
                    errors = vex.Errors
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    };
}