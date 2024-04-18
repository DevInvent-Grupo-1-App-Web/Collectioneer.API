using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Tag
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;
		
		// Navigation properties
		public Community? Community { get; set; }
		public ICollection<PostTag> PostTags { get; set; } = [];

		public Tag(
			int communityId,
			string name,
			string value
		)
		{
			CommunityId = communityId;
			Name = name;
			Value = value;
		}
	}
}