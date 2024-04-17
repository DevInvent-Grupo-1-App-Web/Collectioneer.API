namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Filter
	{
		public int Id { get; set; }
		public int CommunityId { get; set; }
		public string Name { get; set; }
		public FilterType Type { get; set; }
	}
}