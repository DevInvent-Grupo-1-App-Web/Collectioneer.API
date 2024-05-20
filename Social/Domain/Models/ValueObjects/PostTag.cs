using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class PostTag
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public int TagId { get; set; }
		
		// Navigation properties
		public Post? Post { get; set; }
		public Tag? Tag { get; set; }

		public PostTag(
			int postId,
			int tagId
		)
		{
			PostId = postId;
			TagId = tagId;
		}
	}
}