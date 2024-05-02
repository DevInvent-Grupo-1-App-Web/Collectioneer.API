using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Application.External
{
    public record CollectibleDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int OwnerId { get; init; }
        public float? Value { get; init; }
        public int ArticleId { get; init; }
        public bool IsLinkedToProcess { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public CollectibleDTO(Collectible collectible)
        {
            Id = collectible.Id;
            Name = collectible.Name;
            OwnerId = collectible.OwnerId;
            Value = collectible.Value;
            ArticleId = collectible.ArticleId;
            IsLinkedToProcess = collectible.IsLinkedToProcess();
            CreatedAt = collectible.CreatedAt;
            UpdatedAt = collectible.UpdatedAt;
        }
    }
}
