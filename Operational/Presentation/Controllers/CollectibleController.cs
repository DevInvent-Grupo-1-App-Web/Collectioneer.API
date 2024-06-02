using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
    [ApiController]
    public class CollectibleController : ControllerBase
    {
        private readonly ICollectibleService _collectibleService;
		private readonly ICommentService _commentService;
        private readonly ILogger<CollectibleController> _logger;

        public CollectibleController(ICollectibleService collectibleService, ILogger<CollectibleController> logger, ICommentService commentService)
        {
            _collectibleService = collectibleService;
            _logger = logger;
			_commentService = commentService;
        }

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
        [HttpPost("collectibles")]
        public async Task<ActionResult<CollectibleDTO>> CreateCollectible([FromBody] CollectibleRegisterCommand request)
        {
            try
            {
                var collectible = await _collectibleService.RegisterCollectible(request);
                return CreatedAtAction(nameof(GetCollectible), new { id = collectible.Id }, collectible);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating collectible.");
                return StatusCode(500, ex.Message);
            }
        }
	

		[HttpGet("collectibles/{id}/comments")]
		public async Task<ActionResult<ICollection<CommentDTO>>> GetCommentsForCollectible([FromRoute] int id)
		{
			try
			{
				var response = await _commentService.GetCommentsForCollectible(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting comments for collectible.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("collectibles/{id}/comments")]
		public async Task<ActionResult<CommentDTO>> CreateCommentForCollectible([FromRoute] int id, [FromBody] CommentRegisterCommand request)
		{
			try
			{
				request.CollectibleId = id;
				await _commentService.PostComment(request);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating comment for collectible.");
				return StatusCode(500, ex.Message);
			}
		}
	}
}
