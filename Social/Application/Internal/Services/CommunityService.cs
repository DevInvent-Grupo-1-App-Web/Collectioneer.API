using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services
{
    public class CommunityService(
        ICommunityRepository communityRepository,
        IUnitOfWork unitOfWork,
        IRoleService roleService,
        IRoleTypeService roleTypeService
    ) : ICommunityService
    {
        public async Task AddUserToCommunity(CommunityJoinCommand command)
        {
            await roleService.CreateNewRole(new CreateRoleCommand(command.UserId, command.CommunityId, "User"));
            await unitOfWork.CompleteAsync();
        }

        public async Task<CommunityDTO> CreateNewCommunity(CommunityCreateCommand command)
        {
            var newCommunity = new Community(command.Name, command.Description);

            try
            {
                await communityRepository.Add(newCommunity);
                await unitOfWork.CompleteAsync();

                await roleService.CreateNewRole(new CreateRoleCommand(command.UserId, newCommunity.Id, "Owner"));
                await unitOfWork.CompleteAsync();

                return new CommunityDTO(newCommunity);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating community.", ex);
            }
        }

        public async Task<ICollection<CommunityDTO>> GetCommunities()
        {
            var communities = await communityRepository.GetAll();
            return communities.Select(c => new CommunityDTO(c)).ToList();
        }

        public async Task<CommunityDTO> GetCommunity(CommunityGetCommand command)
        {
            var community = await communityRepository.GetById(command.Id);
            if (community == null)
            {
                throw new Exception("Community not found.");
            }
            return new CommunityDTO(community);
        }

		public async Task<ICollection<CommunityDTO>> GetUserCommunities(CommunityFetchByUserQuery query)
		{
			var communities = await roleService.GetUserRoles(query.UserId) ?? throw new Exception("User is has not registered in any communities.");
			var communitiesId = communities.Select(c => c.CommunityId).ToList();
			var userCommunities = new List<Community>();
		
			foreach (var id in communitiesId)
			{
				var community = await communityRepository.GetById(id);
				if (community != null)
				{
					userCommunities.Add(community);
				}
			}
		
			return userCommunities.Select(c => new CommunityDTO(c)).ToList();
		}
	}
}
