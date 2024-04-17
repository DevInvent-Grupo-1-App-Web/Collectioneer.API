using Collectioneer.API.Shared.Domain.Interfaces;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Reaction : ITimestamped
	{
		public int Id { get; set; }
		public int ReactableId { get; set; }
		public string ReactableType { get; set; }
		public int UserId { get; set; }
		public ReactionType Type { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}