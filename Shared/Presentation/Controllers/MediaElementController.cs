using Collectioneer.API.Shared.Application.External.Objects;
using Collectioneer.API.Shared.Application.External.Services;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Queries;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers;

[ApiController]
public class MediaElementController : ControllerBase
{
	private readonly IMediaElementService _mediaElementService;
	private readonly IUserService _userService;
	private readonly ILogger<MediaElementController> _logger;

	public MediaElementController(
		IMediaElementService mediaElementService,
		ContentModerationService contentModerationService,
		ILogger<MediaElementController> logger,
		IUserService userService
	)
	{
		_mediaElementService = mediaElementService;
		_logger = logger;
		_userService = userService;
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

		var result = await _mediaElementService.UploadMedia(command, await _userService.GetIdFromRequestHeader());
		return Ok(result);
	}

	[Authorize]
	[HttpGet("media")]
	public async Task<IActionResult> GetMediaElements([FromQuery] GetMediaElementsQuery query)
	{
		ICollection<MediaElement> mediaElements;
		switch (query.ElementType)
		{
			case "collectible":
				mediaElements = await _mediaElementService.GetMediaElementsByCollectibleId(query.ElementId);
				break;
			case "community":
				mediaElements = await _mediaElementService.GetMediaElementsByCommunityId(query.ElementId);
				break;
			case "post":
				mediaElements = await _mediaElementService.GetMediaElementsByPostId(query.ElementId);
				break;
			case "profile":
				mediaElements = await _mediaElementService.GetMediaElementsByProfileId(query.ElementId);
				break;
			default:
				return BadRequest("Invalid element type");
		}

		return Ok(mediaElements.Select(mediaElement => new MediaElementDTO(
	mediaElement.Id,
	mediaElement.UploaderId,
	mediaElement.MediaName,
	mediaElement.MediaURL,
	mediaElement.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
	mediaElement.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
)).ToList());
	}
}