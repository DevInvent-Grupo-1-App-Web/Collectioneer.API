using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;

namespace Collectioneer.API;

public interface IAuctionService
{
	public Task<int> CreateAuction(AuctionCreationCommand command);
	public Task<int> PlaceBid(BidCreationCommand command);

	public Task<IEnumerable<Bid>> GetBids(BidRetrieveQuery query);
}
