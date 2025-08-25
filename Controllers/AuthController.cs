using CinemaSolutionApi.Dtos.User;
using CinemaSolutionApi.Services;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSolutionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            catch (IdentityValidationEx ex)
            {
                return BadRequest(new { errors = ex.Errors });
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
                return Ok(await _authService.LogIn(user));
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