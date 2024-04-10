using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Models.ValueObjects
{
	public record Bid
	{
		public int Id { get; init; }
		public int AuctionId { get; init; }
		public Auction Auction { get; init; }
		public int BidderId { get; init; }
		public User Bidder { get; init; }
		public float Amount { get; init; }
		public DateTime Time { get; init; }
	}
}