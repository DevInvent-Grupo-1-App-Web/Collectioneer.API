using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Social.Presentation.Controllers
{
	[ApiController]
	public class PostController(IPostService postService, ILogger<PostController> logger) : ControllerBase
	{

		[HttpPost("new-post")]
		public async Task<ActionResult<PostDTO>> AddPost([FromBody] AddPostCommand request)
		{
			try
			{
				var newPost = await postService.AddPost(request);
				return Ok(newPost);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error creating post.");
				return StatusCode(500, ex.Message);
			}

		}
	}

}