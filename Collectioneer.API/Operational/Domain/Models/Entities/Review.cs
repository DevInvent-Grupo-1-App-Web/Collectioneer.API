using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
	public class Review : IReactable, ITimestamped
	{
		public int Id { get; set; }
		public int ReviewerId { get; set; }
		public int ReviewedItemId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User Reviewer { get; set; }
		public Article ReviewedItem { get; set; }
		public ICollection<Reaction?> Reactions { get; set; } = [];
	}

}

