namespace Collectioneer.API.Operational.Domain.Commands
{
    public record AuctionValidationCommand
    {
        public int AuctionId { get; init; }
    }
}
