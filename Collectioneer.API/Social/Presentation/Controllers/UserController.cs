﻿using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Collectioneer.API.Social.Presentation.Controllers
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

        // GET api/v1/users
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _userService.GetUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users.");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
