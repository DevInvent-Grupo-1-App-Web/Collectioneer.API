using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application
{
    public class CommunityService(
        ICommunityRepository communityRepository,
        IUnitOfWork unitOfWork
            ) : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository = communityRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task CreateNewCommunity(CommunityCreateCommand command)
        {
            var newCommunity = new Community(command.Name, command.Description);
            
            try
            {
                await _communityRepository.Add(newCommunity);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating community.", ex);
            }
        }
    }
}
