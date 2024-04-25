using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Domain.Repositories
{
    public interface IAuctionRepository : IBaseRepository<Auction>
    {
		public new Task<Auction?> GetById(int id);
    }
}
