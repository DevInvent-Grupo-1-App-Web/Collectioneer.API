using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Repositories
{
    public interface ICommunityRepository : IBaseRepository<Community>
    {
        public Task<ICollection<Community>> Search(string searchTerm);
        public Task<bool> Delete(int id);
    }
}
