namespace Collectioneer.API.Operational.Domain.Commands
{
    public record ArticleCreationCommand
    {
        public string CollectibleId { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
    }
}
