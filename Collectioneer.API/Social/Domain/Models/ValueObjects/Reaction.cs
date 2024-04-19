using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Abstracts;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Reaction : ITimestamped
	{
		public int Id { get; set; }
		public int InteractableId { get; set; }
		public int UserId { get; set; }
		public int ReactionTypeId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? User { get; set; }
		public Interactable? Interactable { get; set; }

        public ReactionType? ReactionType { get; set; }

        public Reaction(
			int interactableId,
			int userId,
			int reactionTypeId
		)
		{
			InteractableId = interactableId;
			UserId = userId;
			ReactionTypeId = reactionTypeId;
		}
	}
}