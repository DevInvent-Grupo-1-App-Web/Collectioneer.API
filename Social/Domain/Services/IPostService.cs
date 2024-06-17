using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;

namespace Collectioneer.API.Social.Domain.Services {
	public interface IPostService {
		public Task<PostDTO> AddPost(AddPostCommand command);
		public Task<ICollection<PostDTO>> Search(string searchTerm);
	}
}