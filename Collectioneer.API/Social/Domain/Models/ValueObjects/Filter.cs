using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Filter
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public FilterType Type { get; set; }

		// Navigation properties
		public Community? Community { get; set; }

		public Filter(
			int communityId,
			string name,
			FilterType type
		)
		{
			CommunityId = communityId;
			Name = name;
			Type = type;
		}
	}
}