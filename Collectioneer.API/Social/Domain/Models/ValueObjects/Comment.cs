using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Abstracts;
using Collectioneer.API.Social.Domain.Interfaces;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Comment : Reactable, ICommentable, ITimestamped
	{
		public int Id { get; set; }
		public int CommentParentId { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsHidden { get; set; } = false;

		// Navigation properties
		public CommentParent? CommentParent{ get; set; }
		public User? User { get; set; }
		public ICollection<Comment> Comments { get; set; } = [];

		public Comment(
			int commentParentId,
			int userId,
			string content
		)
		{
			CommentParentId = commentParentId;
			UserId = userId;
			Content = content;
		}
	}
}