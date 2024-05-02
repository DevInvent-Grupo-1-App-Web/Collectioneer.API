namespace Collectioneer.API.Operational.Domain.Queries
{
    public class AuctionGetByCollectibleIdQuery(
        int CollectibleId    
    )
    {
        public int CollectibleId { get; init; } = CollectibleId;
    }
}
