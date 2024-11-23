using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services {
	public class PostService(IPostRepository postRepository, IUnitOfWork unitOfWork) : IPostService
	{
		public async Task<PostDTO> AddPost(AddPostCommand command) {
			var post = new Post(command.CommunityId, command.Title, command.Content, command.AuthorId);
			try
			{
				await postRepository.Add(post);
				await unitOfWork.CompleteAsync();
				return new PostDTO(post);
			}
			catch (Exception ex)
			{
				throw new Exception("Unknown error creating post.", ex);
			}
		}

		public async Task<PaginatedResult<PostDTO>> Search(PostSearchQuery query)
		{
			var paginatedPosts = await postRepository.Search(query.SearchTerm, query.CommunityId, query.Page, query.PageSize);
			var productDTOs = paginatedPosts.Items
				.Select(p => new PostDTO(p))
				.ToList();
			
			return new PaginatedResult<PostDTO>
			{
				Items = productDTOs,
				CurrentPage = paginatedPosts.CurrentPage,
				PageSize = paginatedPosts.PageSize,
				TotalPages = paginatedPosts.TotalPages
			};
		}

		public async Task<PostDTO?> GetPost(int postId)
		{
            var post = await postRepository.GetById(postId);
			return post == null ? null : new PostDTO(post);
        }
	}
}