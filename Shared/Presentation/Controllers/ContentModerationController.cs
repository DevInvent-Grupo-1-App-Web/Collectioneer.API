using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
	[ApiController]
	public class ContentModerationController : ControllerBase
	{
		private readonly IContentModerationService _contentModerationService;
		private readonly ILogger<ContentModerationController> _logger;

		public ContentModerationController(IContentModerationService contentModerationService, ILogger<ContentModerationController> logger)
		{
			_contentModerationService = contentModerationService;
			_logger = logger;
		}

		[HttpPost("text")]
		public async Task<IActionResult> IsTextContentSafe([FromBody] string content)
		{
			try
			{
				var result = await _contentModerationService.ScreenTextContent(content);
				return Ok(result);
			}
			catch (Microsoft.Azure.CognitiveServices.ContentModerator.Models.APIErrorException apiEx)
			{
				_logger.LogError(apiEx, "API error while checking text content safety: {Message}", apiEx.Message);
				return BadRequest(new { error = "Content moderation API error", details = apiEx.Message });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while checking text content safety.");
				return StatusCode(500, "Internal server error.");
			}
		}
	}
}