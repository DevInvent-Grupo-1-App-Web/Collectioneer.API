using Collectioneer.API.Shared.Application.External.Services;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers;

[ApiController]
public class MediaElementController : ControllerBase
{
	private readonly IMediaElementService _mediaElementService;
	private readonly ContentModerationService _contentModerationService;
    private readonly ILogger<MediaElementController> _logger;

	public MediaElementController(
		IMediaElementService mediaElementService,
		ContentModerationService contentModerationService,
		ILogger<MediaElementController> logger
	)
	{
		_mediaElementService = mediaElementService;
		_contentModerationService = contentModerationService;
		_logger = logger;
	}
	
	[Authorize]
	[HttpPost("upload-media")]
	[RequestSizeLimit(16_000_000)]
	public async Task<IActionResult> UploadMedia([FromBody] MediaElementUploadCommand command)
	{
		if (command.ContentType.StartsWith("image/") == false && command.ContentType.StartsWith("video/") == false)
		{
			return BadRequest("Only images and videos are allowed");
		}

		try
		{
			if (command.ContentType.StartsWith("image/"))
			{
				if (await _contentModerationService.IsImageContentSafe(Convert.FromBase64String(command.Content)) == false)
				{
					return BadRequest("Image content is not safe. Suspected inappropriate content detected.");
				}
			}
		}
		catch (Exception)
		{
			_logger.LogWarning("Content moderation service is not available. Skipping content moderation check.");	
		}

		var result = await _mediaElementService.UploadMedia(command);
		return Ok(result);
	}
}