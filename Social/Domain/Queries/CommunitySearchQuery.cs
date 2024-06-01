namespace Collectioneer.API.Social.Domain.Queries
{
    public record CommunitySearchQuery(
        string SearchTerm
    ) {
        public string SearchTerm { get; init; } = SearchTerm;
    }
}