namespace Collectioneer.API.Operational.Domain.Commands
{
    public record ArticleCreationCommand
    {
        public int CollectibleId { get; init; }
        public string Title { get; init; }
    }
}
