using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application
{
    public class RoleTypeService(
                   IRoleTypeRepository roleTypeRepository,
                              IUnitOfWork unitOfWork
                   ) : IRoleTypeService
    {
        public async Task CreateNewRoleType(CreateRoleTypeCommand command)
        {
            try
            {
                await roleTypeRepository.CreateNewRoleType(command.Name);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating role type.", ex);
            }
        }
    }
}
