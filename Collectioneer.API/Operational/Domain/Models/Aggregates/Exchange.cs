using Collectioneer.API.Operational.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.Aggregates
{
	public class Exchange : ITransaction, ITimestamped
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public int ExchangerId {get; set; }
		public int CollectibleId { get; set; }
		public float Price { get; set; }
		public bool IsOpen { get; set; } = true;
		public bool ExchangerHasConfirmed { get; set; } = false;
		public int? AcceptedExchangeId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Exchanger { get; set; }
		public ICollection<int> ProposedExchanges { get; set; } = [];

		public Exchange(
			int communityId,
			int exchangerId,
			int collectibleId,
			float price
		)
		{
			CommunityId = communityId;
			ExchangerId = exchangerId;
			CollectibleId = collectibleId;
			Price = price;
		}
	}
}
