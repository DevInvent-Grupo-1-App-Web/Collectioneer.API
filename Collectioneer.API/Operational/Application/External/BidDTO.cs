using Collectioneer.API.Operational.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Application.External
{
    public class BidDTO
    {
		public int Id { get; init; }
		public int AuctionId { get; init; }
		public float Amount { get; init; }
		public DateTime CreatedAt { get; init; }

		public BidDTO(Bid bid)
		{
			Id = bid.Id;
			AuctionId = bid.AuctionId;
			Amount = bid.Amount;
			CreatedAt = bid.CreatedAt;
		}
	}
}