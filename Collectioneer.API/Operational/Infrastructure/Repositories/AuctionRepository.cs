using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
	public class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
	{
		public AuctionRepository(AppDbContext context) : base(context)
		{

		}

		public new async Task<Auction?> GetById(int id)
		{
			return await _context.Auctions
				.AsNoTracking()
				.FirstOrDefaultAsync(a => a.Id == id);
		}
	}
}
