namespace Collectioneer.API.Operational.Domain.Queries
{
	public record CollectibleSearchQuery(
		string SearchTerm,
		int CommunityId = 0,
		int Page = 1,
		int PageSize = 10
	)
	{
		public string SearchTerm { get; init; } = SearchTerm;
		public int CommunityId { get; init; } = CommunityId;
		public int Page { get; init; } = Page;
		public int PageSize { get; init; } = PageSize;
	}
}