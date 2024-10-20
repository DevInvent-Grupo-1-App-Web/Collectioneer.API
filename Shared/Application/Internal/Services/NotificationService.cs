using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Application.External.Services;

namespace Collectioneer.API.Shared.Application.Internal.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IUserRepository _userRepository;
        private readonly CommunicationService _communicationService;

        public NotificationService(
            IAuctionRepository auctionRepository,
            IBidRepository bidRepository,
            IUserRepository userRepository,
            CommunicationService communicationService)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
            _userRepository = userRepository;
            _communicationService = communicationService;
        }

        public async Task SendAuctionNotificationAsync(int auctionId)
        {
            //Get the auction by Id
            var auction = await _auctionRepository.GetById(auctionId);
            if (auction == null) throw new ArgumentException("Auction not found.");
            
            //Get all bids of the auction
            var bids = await _bidRepository.GetBidsByAuctionId(auctionId);
            if (!bids.Any()) throw new ArgumentException("No bids found for the auction");
            
            //Get the auctioneer user
            var auctioneer = await _userRepository.GetByIdAsync(auction.AuctioneerId);
            if (auctioneer == null) throw new ArgumentException("Auctioneer not found.");
            
            //Get last bid user
            var lastBid = bids.OrderByDescending(b => b.CreatedAt).FirstOrDefault();
            if (lastBid == null) throw new ArgumentException("No bids found for the auction");
            var lastBidder = await _userRepository.GetByIdAsync(userId: lastBid.BidderId);
            if (lastBidder == null) throw new ArgumentException("Bidder not found.");
            
            // Send emails to users
            string subject = $"Notification of Auction #{auctionId}";
            string bodyAuctioneer = $"Hi {auctioneer.Name}, the auction was closed. And {lastBidder.Name} was the last bid.";
            string bodyBidder = $"Hi {lastBidder.Name}, your bid was the las one for Auction #{auctionId}.";
            
            // Send email to auctioneer
            await _communicationService.SendEmail(auctioneer.Name, subject, bodyAuctioneer);
            
            // Send email to last bidder
            await _communicationService.SendEmail(lastBidder.Name, subject, bodyBidder);

            //Send email to all bidders
            foreach (var bid in bids)
            {
                var bidder = await _userRepository.GetByIdAsync(bid.BidderId);
                if (bidder == null) throw new ArgumentException("Bidder not found.");
                if (bidder.Id == lastBidder.Id) continue;
                string body = $"Hi {bidder.Name}, the auction was closed. And {lastBidder.Name} was the last bid.";
                await _communicationService.SendEmail(bidder.Name, subject, body);
            }
        }

    }
}

