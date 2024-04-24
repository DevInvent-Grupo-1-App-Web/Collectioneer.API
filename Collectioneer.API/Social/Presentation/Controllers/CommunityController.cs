using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CommunityController(ICommunityService communityService, ILogger<UserController> logger, IUserService userService) : ControllerBase
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

        [HttpPost("join-community")]
        public async Task<IActionResult> JoinCommunity([FromBody] CommunityJoinCommand request)
        {
            try
            {
                await communityService.AddUserToCommunity(request);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error joining community.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
