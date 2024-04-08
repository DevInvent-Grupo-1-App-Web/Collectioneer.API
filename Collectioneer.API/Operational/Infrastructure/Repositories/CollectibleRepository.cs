using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using PhoneResQ.API.Shared.Infrastructure.Repositories;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
    public class CollectibleRepository : BaseRepository<Collectible>, ICollectibleRepository
    {
        public CollectibleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
