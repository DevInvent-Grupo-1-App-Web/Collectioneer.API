namespace Collectioneer.API.Operational.Domain.Commands
{
    public record CollectibleRegisterCommand
    {
        public string Name { get; init; }
        public int OwnerId { get; init; }
        public float? Value { get; init; }
        public int CommunityId { get; init; }
        public int ArticleId { get; init; }
    }
}
