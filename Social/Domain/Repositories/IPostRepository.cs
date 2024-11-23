using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
		public Task<ICollection<Post>> GetPosts(int communityId);
        public Task<PaginatedResult<Post>> Search(string searchTerm, int communityId, int page = 1, int pageSize = 10);
    }
}
