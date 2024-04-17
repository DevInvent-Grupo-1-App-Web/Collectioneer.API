using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Tag
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		
		// Navigation properties
		public Community Community { get; set; }
		public ICollection<PostTag> PostTags { get; set; }
	}
}