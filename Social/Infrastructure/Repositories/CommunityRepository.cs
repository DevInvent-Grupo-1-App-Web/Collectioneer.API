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
    }
}
