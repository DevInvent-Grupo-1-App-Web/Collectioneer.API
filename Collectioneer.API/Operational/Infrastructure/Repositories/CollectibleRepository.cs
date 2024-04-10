using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Exceptions;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
    public class CollectibleRepository : BaseRepository<Collectible>, ICollectibleRepository
    {
        public CollectibleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task DeleteUserCollectibles(int userId)
        {
            var collectibles = await _context.Collectibles.Where(c => c.OwnerId == userId).ToListAsync();

            foreach (var collectible in collectibles)
            {
                if (collectible.IsLinkedToProcess())
                {
                    throw new ModelIntegrityException("Collectible is currently in active process. Cannot delete.");
                }
                _context.Collectibles.Remove(collectible);
            }

            await _context.SaveChangesAsync();
        }
    }
}
