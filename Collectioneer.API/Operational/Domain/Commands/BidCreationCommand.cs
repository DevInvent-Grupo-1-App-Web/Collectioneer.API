namespace Collectioneer.API.Operational.Domain.Commands
{
	public record class BidCreationCommand
	{
		public int AuctionId { get; init; }
		public int BidderId { get; init; }
		public float Amount { get; init; }
	}

}
