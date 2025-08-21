using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Services;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSolutionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post(SignUpUserDto user)
        {
            try
            {
                var result = await _authService.CreateUser(user);
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

        [HttpPost("login")]
        public async Task<IActionResult> Post(LogInDto user)
        {
            try
            {
                var result = await _authService.LogIn(user);
                return Ok(new
                {
                    tkn_cinema = result[0],
                    username = result[1],
                });
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