using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Queries;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Collectioneer.API.Shared.Presentation.Controllers
{
	[Route("api/v1")]
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

		[HttpGet("user/{id}")]
		public async Task<IActionResult> GetUser([FromRoute] int id)
		{
            try
			{
                var response = await _userService.GetUser(id);
                return Ok(response);
            }
            catch (Exception ex)
			{
                _logger.LogError(ex, "Error getting user.");
                return StatusCode(500, ex.Message);
            }
        }


		[HttpPost("register-user")]
		public async Task<IActionResult> RegisterUser([FromBody] UserRegisterCommand request)
		{
			try
			{
				var response = await _userService.RegisterNewUser(request);
                return CreatedAtRoute(nameof(GetUser), new { id = response.Id }, response);

            }
            catch (Exception ex)
			{
				_logger.LogError(ex, "Error registering user.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginUser([FromBody] UserLoginQuery request)
		{
			try
			{
				var response = await _userService.LoginUser(request);
				return Ok(new { token = response, type = "Bearer"});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error logging in user.");
				return StatusCode(500, ex.Message);
			}
		}

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
				if (ex.GetType() == typeof(UserNotFoundException)) return NotFound(ex.Message);

				if (ex.GetType() == typeof(ModelIntegrityException)) return BadRequest(ex.Message);

				return StatusCode(500, ex.Message);
            }
        }

	}
}
