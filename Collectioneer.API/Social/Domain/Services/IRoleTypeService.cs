using Collectioneer.API.Social.Domain.Commands;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface IRoleTypeService
    {
        public Task CreateNewRoleType(CreateRoleTypeCommand command);
    }
}
