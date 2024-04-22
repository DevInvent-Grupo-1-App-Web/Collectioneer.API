using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application
{
    public class RoleService(
        IRoleRepository roleRepository,
        IRoleTypeRepository roleTypeRepository,
        IUnitOfWork unitOfWork
    ) : IRoleService
    {
        public async Task CreateNewRole(CreateRoleCommand command)
        {
            var roletype = await roleTypeRepository.GetRoleTypeByName(command.RoleType) ?? throw new Exception("RoleType not found.");

            var newRole = new Role(command.UserId, command.CommunityId, roletype );
            
            try
            {
                await roleRepository.Add(newRole);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating role.", ex);
            }
        }
    }
}
