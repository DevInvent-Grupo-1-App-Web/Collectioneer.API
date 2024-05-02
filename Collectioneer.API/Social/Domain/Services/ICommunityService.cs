using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface ICommunityService
    {
        public Task<CommunityDTO> CreateNewCommunity(CommunityCreateCommand command);
        public Task AddUserToCommunity(CommunityJoinCommand command);
        public Task<CommunityDTO> GetCommunity(CommunityGetCommand command);
        public Task<ICollection<CommunityDTO>> GetCommunities();
    }
}
