namespace Collectioneer.API.Operational.Domain.Commands
{
	public record class AuctionCreationCommand
	{
		public int AuctioneerId { get; init; }
		public int CollectibleId { get; init; }
		public float StartingPrice { get; init; }
		public DateTime Deadline { get; init; }
	}
}
