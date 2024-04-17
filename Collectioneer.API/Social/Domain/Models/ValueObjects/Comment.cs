using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Comment : ICommentable, IReactable, ITimestamped
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsHidden { get; set; }

		// Navigation properties
		public Post? Post { get; set; }
		public User User { get; set; }
		public ICollection<Reaction> Reactions { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}