using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services {
	public class PostService : IPostService
	{
		private readonly IPostRepository _postRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			_postRepository = postRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<PostDTO> AddPost(AddPostCommand command) {
			var post = new Post(command.CommunityId, command.Title, command.Content, command.AuthorId);
			try
			{
				await _postRepository.Add(post);
				await _unitOfWork.CompleteAsync();
				return new PostDTO(post);
			}
			catch (Exception ex)
			{
				throw new Exception("Unknown error creating post.", ex);
			}
		}

		public async Task<ICollection<PostDTO>> Search(PostSearchQuery query)
		{
			var posts = await _postRepository.Search(query.SearchTerm, query.CommunityId);
			return posts.Select(p => new PostDTO(p)).ToList();
		}

		public async Task<PostDTO?> GetPost(int postId)
		{
            var post = await _postRepository.GetById(postId);
			return post == null ? null : new PostDTO(post);
        }
	}
}