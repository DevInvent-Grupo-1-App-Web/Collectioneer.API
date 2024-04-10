using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Domain.Repositories
{
    public interface ICollectibleRepository : IBaseRepository<Collectible>
    {
        public Task DeleteUserCollectibles(int userId);
    }
}
