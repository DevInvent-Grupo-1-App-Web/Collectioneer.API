using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Exceptions;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
    public class Collectible
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public float? Value { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int? AuctionId { get; set; }
        public Auction? Auction { get; set; }

        public Collectible(
                    string name,
                    int ownerId,
                    float? value
                )
        {
            SetName(name);
            SetOwnerId(ownerId);
            SetValue(value);
        }

        public bool IsLinkedToProcess()
        {
            // Verify if in auction
            return AuctionId != null;

            // TODO: Verify if in other processes when implemented
        }

        private void SetName(string name)
        {
            if (name.Length < 3 || name.Length > 100)
            {
                throw new CollectibleModelException("Name must be between 1 and 100 characters");
            }
            Name = name;
        }

        private void SetOwnerId(int ownerId)
        {
            OwnerId = ownerId;
        }

        private void SetValue(float? value)
        {
            Value = value ?? null;
        }
    }
}
