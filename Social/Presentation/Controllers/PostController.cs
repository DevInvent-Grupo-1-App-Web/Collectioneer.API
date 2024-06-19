using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Queries;
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

		[HttpGet("posts/{postId}")]
		public async Task<ActionResult<PostDTO>> GetPost([FromRoute] int postId)
		{
            try
			{
                var post = await postService.GetPost(postId) ?? throw new Exception("Post not found.");
                return Ok(post);
            }
            catch (Exception ex)
			{
                logger.LogError(ex, "Error getting post.");
                return StatusCode(500, ex.Message);
            }
        }

		[HttpGet("search/posts")]
		public async Task<ActionResult<ICollection<PostDTO>>> SearchPosts([FromQuery] PostSearchQuery query)
		{
            try
			{
                var posts = await postService.Search(query);
                return Ok(posts);
            }
            catch (Exception ex)
			{
                logger.LogError(ex, "Error searching posts.");
                return StatusCode(500, ex.Message);
            }
		}
	}

}