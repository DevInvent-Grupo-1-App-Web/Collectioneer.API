namespace Collectioneer.API.Operational.Domain.Commands
{
    public record CollectibleAuctionIdRegisterCommand
    {
        public int CollectibleId { get; init; }
        public int AuctionId { get; init; }
    }
}
