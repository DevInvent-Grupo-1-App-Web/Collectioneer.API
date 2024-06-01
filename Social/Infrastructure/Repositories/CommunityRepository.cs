using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        public CommunityRepository(AppDbContext context) : base(context)
        {
        }

        public Task<ICollection<Community>> SearchCommunities(string searchTerm)
        {
            searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

            var communities = await context.Communities.Select(c => c.Name.Contains(searchTerm)).ToListAsync();

            return communities;
        }
    }
}
