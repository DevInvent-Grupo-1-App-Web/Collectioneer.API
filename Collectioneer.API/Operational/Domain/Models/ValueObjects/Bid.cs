using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.ValueObjects
{
    public class Bid(
        int AuctionId,
        int BidderId,
        float Amount,
        DateTime Time
    )
    {
        public int Id { get; set; }
        public int AuctionId { get; set; } = AuctionId;
        public Auction Auction { get; set; }
        public int BidderId { get; set; } = BidderId;
        public User Bidder { get; set; }
        public float Amount { get; set; } = Amount;
        public DateTime Time { get; set; } = Time;
    }
}