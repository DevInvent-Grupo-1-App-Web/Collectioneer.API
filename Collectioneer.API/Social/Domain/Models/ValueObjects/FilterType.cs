namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class FilterType
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		// Navigation properties
		public ICollection<Filter> Filters { get; set; } = [];

		public FilterType(
			string name
		)
		{
			Name = name;
		}
	}
}