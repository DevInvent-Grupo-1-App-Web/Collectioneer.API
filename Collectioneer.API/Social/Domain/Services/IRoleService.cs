using Collectioneer.API.Social.Domain.Commands;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface IRoleService
    {
        public Task CreateNewRole(CreateRoleCommand command);
    }
}
