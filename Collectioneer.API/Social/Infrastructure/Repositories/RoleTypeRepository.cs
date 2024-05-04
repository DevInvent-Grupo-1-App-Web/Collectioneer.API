using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class RoleTypeRepository : BaseRepository<RoleType>, IRoleTypeRepository
    {
        public RoleTypeRepository(AppDbContext context) : base(context) { }

        public async Task CreateNewRoleType(string name)
        {
            var roleType = new RoleType(name);
            await _context.RoleTypes.AddAsync(roleType);
            await _context.SaveChangesAsync();
        }

        public async Task<RoleType> GetRoleTypeByName(string name)
        {
            return await _context.RoleTypes.FirstOrDefaultAsync(rt => rt.Name == name);
        }
    }
}
