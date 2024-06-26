using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Filter
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public int FilterTypeId { get; set; }

		// Navigation properties
		public Community? Community { get; set; }
		public FilterType? FilterType { get; set; }

		public Filter(
			int communityId,
			string name,
			int filterTypeId
		)
		{
			CommunityId = communityId;
			Name = name;
            FilterTypeId = filterTypeId;
		}
	}
}