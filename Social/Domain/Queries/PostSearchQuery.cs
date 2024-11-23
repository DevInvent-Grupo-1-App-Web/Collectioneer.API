namespace Collectioneer.API.Social.Domain.Queries
{
    public record PostSearchQuery (
        string SearchTerm,
        int CommunityId,
        int Page = 1,
        int PageSize = 10
    )
    {
        public string SearchTerm { get; init; } = SearchTerm;
        public int CommunityId { get; init;  } = CommunityId;
        public int Page { get; init; } = Page;
        public int PageSize { get; init; } = PageSize;
    }
}
