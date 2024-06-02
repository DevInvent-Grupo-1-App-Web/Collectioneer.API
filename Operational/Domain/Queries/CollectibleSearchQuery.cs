namespace Collectioneer.API.Operational.Domain.Queries
{
    public record CollectibleSearchQuery(
        string SearchTerm
    ) {
        public string SearchTerm { get; init; } = SearchTerm;
    }
}