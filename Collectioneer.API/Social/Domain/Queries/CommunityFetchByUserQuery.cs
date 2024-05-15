namespace Collectioneer.API.Social.Domain.Queries
{
	public class CommunityFetchByUserQuery
	{
		public int UserId { get; set; }

		public CommunityFetchByUserQuery(int userId)
		{
			UserId = userId;
		}
	}
}