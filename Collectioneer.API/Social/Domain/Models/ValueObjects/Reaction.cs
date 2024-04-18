using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Reaction : ITimestamped
	{
		public int Id { get; set; }
		public int ReactionReactableId { get; set; }
		public int UserId { get; set; }
		public ReactionType Type { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		
		// Navigation properties
		public User? User { get; set; }
		public ReactionReactable? ReactionReactable { get; set; }

		public Reaction(
			int reactionReactableId,
			int userId,
			ReactionType type
		)
		{
			ReactionReactableId = reactionReactableId;
			UserId = userId;
			Type = type;
		}
	}
}