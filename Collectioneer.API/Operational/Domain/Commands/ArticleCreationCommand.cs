namespace Collectioneer.API.Operational.Domain.Commands
{
    public record ArticleCreationCommand
		(
			string Title,
			int CollectibleId
		)
    {
        public int CollectibleId { get; init; } = CollectibleId;
        public string Title { get; init; } = Title;
    }
}
