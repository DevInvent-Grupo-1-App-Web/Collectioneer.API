using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
    public class BidRepository : BaseRepository<Bid>, IBidRepository
    {
        public BidRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bid>> GetBidsByAuctionId(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId).ToListAsync();
        }
    }
}
