using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Services.Intern
{
    public interface ICollectibleService
    {
        public Task<int> RegisterCollectible(CollectibleRegisterCommand command);
        public Task RegisterAuctionIdInCollectible(CollectibleAuctionIdRegisterCommand command);
    }
}
