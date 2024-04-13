using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API;

public class AuctionService(
    IAuctionRepository auctionRepository,
    ICollectibleService collectibleService,
    IBidRepository bidRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly ICollectibleService _collectibleService = collectibleService;
    private readonly IBidRepository _bidRepository = bidRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<int> CreateAuction(AuctionCreationCommand command)
    {
        var auction = new Auction(command.AuctioneerId, command.CollectibleId, command.StartingPrice, command.Deadline);
        await _auctionRepository.Add(auction);
        await _unitOfWork.CompleteAsync();

        await _collectibleService.RegisterAuctionIdInCollectible(new CollectibleAuctionIdRegisterCommand { AuctionId = auction.Id, CollectibleId = command.CollectibleId });
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
