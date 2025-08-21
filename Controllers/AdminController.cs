using Microsoft.AspNetCore.Authorization;
using CinemaSolutionApi.Dtos.Admin;
using CinemaSolutionApi.Services;
using CinemaSolutionApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSolutionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("createuser")]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> Post(CreateUserDto newUser)
        {
            try
            {
                var result = await _adminService.CreateUser(newUser);
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

        [HttpGet("user")]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _adminService.GetUsers();
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

        [HttpDelete("user/{id}")]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _adminService.DeleteUser(id);
                return Ok();
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

        [HttpPut("user/{id}")]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> Put(string id, CreateUserDto newValues)
        {
            try
            {
                await _adminService.ModifyUser(id, newValues);
                return Ok();
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
    }
};