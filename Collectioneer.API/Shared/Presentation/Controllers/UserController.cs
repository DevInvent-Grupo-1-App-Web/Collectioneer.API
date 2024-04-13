using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Queries;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Collectioneer.API.Shared.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // POST api/v1/register-user
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterCommand request)
        {
            try
            {
                var response = await _userService.RegisterNewUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/v1/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginQuery request)
        {
            try
            {
                var response = await _userService.LoginUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user.");
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/v1/delete-user
        [Authorize]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] UserDeleteCommand request)
        {
            try
            {
                await _userService.DeleteUser(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user.");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
