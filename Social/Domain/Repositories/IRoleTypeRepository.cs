using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Repositories
{

    public interface IRoleTypeRepository : IBaseRepository<RoleType>
    {
        public Task CreateNewRoleType(string name);
        public Task<RoleType> GetRoleTypeByName(string name);
    }
}
