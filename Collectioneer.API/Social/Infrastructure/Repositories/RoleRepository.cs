using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
