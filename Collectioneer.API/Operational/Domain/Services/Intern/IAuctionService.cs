using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;

namespace Collectioneer.API.Operational.Domain.Services.Intern;

public interface IAuctionService
{
    public Task<int> CreateAuction(AuctionCreationCommand command);
    public Task<int> PlaceBid(BidCreationCommand command);
    public Task<IEnumerable<Bid>> GetBids(BidRetrieveQuery query);
		public Task<Bid?> CloseAuction(AuctionCloseCommand command);
		public Task AuctioneerConfirmation(AuctionValidationCommand command);
		public Task BidderConfirmation(AuctionValidationCommand command);
}
