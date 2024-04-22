﻿using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CommunityController(ICommunityService communityService, ILogger<UserController> logger) : ControllerBase
    {
        [HttpPost("new-community")]
        public async Task<IActionResult> CreateCommunity([FromBody] CommunityCreateCommand request)
        {
            try
            {
                await communityService.CreateNewCommunity(request);
                return Created();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating community.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}