using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Services
{
    public interface ICollectibleService
    {
        public Task<int> RegisterCollectible(CollectibleRegisterCommand command);
    }
}
