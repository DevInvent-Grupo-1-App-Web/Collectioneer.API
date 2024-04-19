using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Abstracts;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Models.Aggregates
{
	public class Post : Interactable, ITimestamped
	{
		public int CommunityId { get; set; }
		public string Title { get; set; } = string.Empty;
		public int AuthorId { get; set; }
		public bool IsHidden { get; set; } = false;
		public bool IsArchived { get; set; } = false;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public Community? Community { get; set; }
		public User? Author { get; set; }
		public ICollection<PostTag> PostTags { get; set; } = [];
		public Post(
			int communityId,
			string title,
			string content,
			int authorId
		)
		{
			CommunityId = communityId;
			Title = title;
			Content = content;
			AuthorId = authorId;
		}
	}
}