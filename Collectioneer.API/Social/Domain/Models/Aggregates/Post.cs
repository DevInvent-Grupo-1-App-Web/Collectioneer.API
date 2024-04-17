using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Models.Aggregates
{
	public class Post : ICommentable, IReactable, IMediaHolder, ITimestamped
	{
		public int Id { get; set;}
		public int CommunityId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int AuthorId { get; set; }
		public bool IsHidden { get; set; }
		public bool IsArchived { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public ICollection<MediaElement?> MediaElements { get; set; } = [];

		// Navigation properties
		public Community Community { get; set; }
		public User Author { get; set; }
		public ICollection<PostTag> PostTags { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<Reaction> Reactions { get; set; }
	}
}