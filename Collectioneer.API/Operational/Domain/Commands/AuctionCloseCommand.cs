namespace Collectioneer.API;

public record AuctionCloseCommand
{
	public int AuctionId { get; init; }
}
