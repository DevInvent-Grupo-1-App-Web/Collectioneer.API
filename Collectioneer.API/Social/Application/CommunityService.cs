using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application
{
    public class CommunityService(
        ICommunityRepository communityRepository,
        IUnitOfWork unitOfWork,
        IRoleService roleService,
        IRoleTypeService roleTypeService
    ) : ICommunityService
    {
        public async Task CreateNewCommunity(CommunityCreateCommand command)
        {
            var newCommunity = new Community(command.Name, command.Description);
            
            try
            {
                await communityRepository.Add(newCommunity);
                await unitOfWork.CompleteAsync();

                await roleService.CreateNewRole(new CreateRoleCommand(command.UserId, newCommunity.Id, "Owner"));
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating community.", ex);
            }
        }
    }
}
