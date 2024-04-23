using Collectioneer.API.Social.Domain.Commands;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface ICommunityService
    {
        public Task CreateNewCommunity(CommunityCreateCommand command);
    }
}
