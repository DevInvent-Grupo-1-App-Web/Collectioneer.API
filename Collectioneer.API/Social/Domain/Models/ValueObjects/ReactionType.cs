namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class ReactionType
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		// Navigation properties
		public ICollection<Reaction> Reactions { get; set; } = [];

		public ReactionType(
			string name
		)
		{
			Name = name;
		}
	}
}