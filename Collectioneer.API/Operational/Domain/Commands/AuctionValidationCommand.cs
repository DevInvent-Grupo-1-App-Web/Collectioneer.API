namespace Collectioneer.API.Operational.Domain.Commands
{
    public record AuctionValidationCommand
		(
			int AuctionId
		)
    {
        public int AuctionId { get; init; } = AuctionId;
    }
}
