using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Commands
{
    public record CollectibleRegisterIntoUserCommand
    {
        public Collectible Collectible { get; init; }
    }
}
