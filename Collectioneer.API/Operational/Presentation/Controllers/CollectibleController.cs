using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
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

        [Authorize]
        [HttpGet("collectibles")]
        public async Task<ActionResult<ICollection<CollectibleDTO>>> GetCollectibles([FromQuery] CollectibleBulkRetrieveQuery request)
        {
            try
            {
                var response = await _collectibleService.GetCollectibles(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting collectibles.");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("collectibles/{id}")]
        public async Task<ActionResult<CollectibleDTO>> GetCollectible([FromRoute] int id)
        {
            try
            {
                var response = await _collectibleService.GetCollectible(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting collectible.");
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/v1/collectibles
        [Authorize]
        [HttpPost("collectibles")]
        public async Task<IActionResult> CreateCollectible([FromBody] CollectibleRegisterCommand request)
        {
            try
            {
                await _collectibleService.RegisterCollectible(request);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating collectible.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
