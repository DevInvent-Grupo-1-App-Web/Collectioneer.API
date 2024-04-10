using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuctionService(IAuctionRepository auctionRepository, IBidRepository bidRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _auctionRepository = auctionRepository;
        _bidRepository = bidRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAuction(AuctionCreationCommand command)
    {
        var auction = _mapper.Map<Auction>(command);
        await _auctionRepository.Add(auction);
        await _unitOfWork.CompleteAsync();
        return auction.Id;
    }

    public async Task<int> PlaceBid(BidCreationCommand command)
    {
        var bid = _mapper.Map<Bid>(command);
        await _bidRepository.Add(bid);
        await _unitOfWork.CompleteAsync();
        return bid.Id;
    }

    public async Task<IEnumerable<Bid>> GetBids(BidRetrieveQuery query)
    {
        return await _bidRepository.GetBidsByAuctionId(query.AuctionId);
    }
}
