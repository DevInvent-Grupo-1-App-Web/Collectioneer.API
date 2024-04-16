using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.Aggregates
{
	public class Auction
	{
		public int Id { get; set; }
		public int AuctioneerId { get; set; }
		public User? Auctioneer { get; set; }
		public int CollectibleId { get; set; }
		public Collectible? Collectible { get; set; }
		public float StartingPrice { get; set; }
		public List<Bid> Bids { get; set; } = [];
		public DateTime Deadline { get; set; }
		public bool IsOpen { get; set; } = true;
		public bool AuctioneerHasCollected { get; set; } = false;
		public bool BidderHasCollected { get; set; } = false;

		public Auction(
				int auctioneerId,
				int collectibleId,
				float startingPrice,
				DateTime deadline
		)
		{
			AuctioneerId = auctioneerId;
			CollectibleId = collectibleId;
			StartingPrice = startingPrice;
			Deadline = deadline;
			Bids = [];
		}

		public float CurrentPrice()
		{
			return Bids.Count == 0 ? StartingPrice : Bids.Last().Amount;
		}

		public Bid? Close()
		{
			if (Bids.Count == 0)
			{
				return null;
			}

			var winningBid = Bids.LastOrDefault();
			IsOpen = false;

			return winningBid;
		}

		public void CollectAuctioneer()
		{
			AuctioneerHasCollected = true;
		}

		public void CollectBidder()
		{
			BidderHasCollected = true;
		}
	}
}