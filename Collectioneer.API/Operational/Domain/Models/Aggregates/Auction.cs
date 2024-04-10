using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Models.Aggregates
{
	public class Auction
	{
		public int Id { get; set; }
		public int AuctioneerId { get; set; }
		public User Auctioneer { get; set; }
		public int CollectibleId { get; set; }
		public Collectible Collectible { get; set; }
		public float StartingPrice { get; set; }
		public float CurrentPrice { get; set; }
		public Stack<Bid> Bids { get; set; }
		public DateTime Deadline { get; set; }

	}
}