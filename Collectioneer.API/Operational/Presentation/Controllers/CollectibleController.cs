using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CollectibleController : ControllerBase
    {
        private readonly ICollectibleService _collectibleService;
        private readonly ILogger<CollectibleController> _logger;

        public CollectibleController(ICollectibleService collectibleService, ILogger<CollectibleController> logger)
        {
            _collectibleService = collectibleService;
            _logger = logger;
        }

        // POST api/v1/collectibles
        [Authorize]
        [HttpPost("collectibles")]
        public async Task<IActionResult> CreateCollectible([FromBody] CollectibleRegisterCommand request)
        {
            try
            {
                var response = await _collectibleService.RegisterCollectible(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating collectible.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
