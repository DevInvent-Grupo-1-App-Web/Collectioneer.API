using Collectioneer.API.Operational.Domain.Interfaces;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.Aggregates
{
	public class Auction : ITimestamped, ITransaction
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public int AuctioneerId { get; set; }
		public int CollectibleId { get; set; }		
		public float StartingPrice { get; set; }
		public DateTime Deadline { get; set; }
		public bool IsOpen { get; set; } = true;
		public bool AuctioneerHasCollected { get; set; } = false;
		public bool BidderHasCollected { get; set; } = false;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		
		// Navigation properties
		public User? Auctioneer { get; set; }
		public Collectible? Collectible { get; set; }
		public List<Bid> Bids { get; set; } = [];
		public Community? Community { get; set; }

		public Auction(
				int communityId,
				int auctioneerId,
				int collectibleId,
				float startingPrice,
				DateTime deadline
		)
		{
			CommunityId = communityId;
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

		public void Close()
		{
			IsOpen = false;
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