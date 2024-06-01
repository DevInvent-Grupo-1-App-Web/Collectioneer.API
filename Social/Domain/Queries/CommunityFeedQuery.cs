namespace Collectioneer.API.Social.Domain.Queries
{
	public class CommunityFeedQuery
	{
		public int CommunityId { get; set; }

		public CommunityFeedQuery(int communityId)
		{
			CommunityId = communityId;
		}
	}
}