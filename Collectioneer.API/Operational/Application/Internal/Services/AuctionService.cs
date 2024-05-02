using AutoMapper;
using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Exceptions;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Operational.Infrastructure.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Collectioneer.API.Operational.Application.Internal.Services;

public class AuctionService(
        IAuctionRepository auctionRepository,
        ICollectibleService collectibleService,
        IBidRepository bidRepository,
        IUnitOfWork unitOfWork) : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly ICollectibleService _collectibleService = collectibleService;
    private readonly IBidRepository _bidRepository = bidRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<int> CreateAuction(AuctionCreationCommand command)
    {
        var auction = new Auction(
            command.CommunityId,
            command.AuctioneerId,
            command.CollectibleId,
            command.StartingPrice,
            command.Deadline);

        await _auctionRepository.Add(auction);
        await _unitOfWork.CompleteAsync();

        await _collectibleService.RegisterAuctionIdInCollectible(new CollectibleAuctionIdRegisterCommand(command.CollectibleId, auction.Id));


        await _unitOfWork.CompleteAsync();

        return auction.Id;
    }

    public async Task<int> PlaceBid(BidCreationCommand command)
    {
        var bid = new Bid(command.AuctionId, command.BidderId, command.Amount);
        var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");

        var lastBid = auction.Bids.LastOrDefault();

        if (lastBid != null && bid.Amount <= lastBid.Amount)
        {
            throw new AuctionModelException("Bid amount must be greater than the last bid amount");
        }

        await _bidRepository.Add(bid);
        await _unitOfWork.CompleteAsync();
        return bid.Id;
    }

    public async Task<IEnumerable<Bid>> GetBids(BidRetrieveQuery query)
    {
        return await _bidRepository.GetBidsByAuctionId(query.AuctionId);
    }

    public async Task<Bid?> CloseAuction(AuctionCloseCommand command)
    {
        var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");
        var bids = await GetBids(new BidRetrieveQuery(command.AuctionId));
        bids = bids.OrderByDescending(b => b.Amount);
        auction.Close();
        await _auctionRepository.Update(auction);
        await _unitOfWork.CompleteAsync();
        return bids.FirstOrDefault();
    }

    public async Task AuctioneerConfirmation(AuctionValidationCommand command)
    {
        var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");
        auction.CollectAuctioneer();
        await _auctionRepository.Update(auction);
        await _unitOfWork.CompleteAsync();
    }

    public async Task BidderConfirmation(AuctionValidationCommand command)
    {
        var auction = await _auctionRepository.GetById(command.AuctionId) ?? throw new EntityNotFoundException("Auction not found");
        auction.CollectBidder();
        await _auctionRepository.Update(auction);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<AuctionDTO> GetAuction(int id)
    {
        var auction = await _auctionRepository.GetById(id);
        if (auction == null)
        {
            throw new EntityNotFoundException("Auction not found");
        }
        return new AuctionDTO(auction);
    }
    public async Task<IEnumerable<AuctionDTO>> GetAuctions(AuctionBulkRetrieveQuery query)
    {
        var auctions = await _auctionRepository.GetAuctions(query.CommunityId, query.MaxAmount, query.Offset);
        return auctions.Select(c => new AuctionDTO(c)).ToList();
    }

    public async Task<AuctionDTO> GetAuctionFromCollectibleId(AuctionGetByCollectibleIdQuery query)
    {
        var auction = await _auctionRepository.GetAuctionByCollectibleId(query.CollectibleId);
        if (auction == null)
        {
            throw new EntityNotFoundException("Auction not found");
        }
        return new AuctionDTO(auction);
    }
}
