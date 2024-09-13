namespace Collectioneer.API.Shared.Domain.Services
{
    public interface INotificationService
    {
        public Task SendAuctionNotificationAsync(int auctionId);
    } 
}

