using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
	public class Review : ITimestamped
	{
		public int Id { get; set; }
		public int ReviewerId { get; set; }
		public int CollectibleId { get; set; }
		public string Content { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Reviewer { get; set; }
		public Collectible? ReviewedCollectible { get; set; }
		public ICollection<Comment>? Comments { get; set; }

		public Review(
			int reviewerId,
			int collectibleId,
			string content,
			int rating
		)
		{
			ReviewerId = reviewerId;
			CollectibleId = collectibleId;
			Content = content;
			Rating = rating;
		}
	}

}

