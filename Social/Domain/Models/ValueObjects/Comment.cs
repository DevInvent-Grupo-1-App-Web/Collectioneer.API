using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Abstracts;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Comment : Interactable, ITimestamped
	{
		public int AuthorId { get; set; }
		public int InteractableId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsHidden { get; set; } = false;

		// Navigation properties
		public Interactable? Interactable { get; set; }
		public User? Author { get; set; }

		public Comment(
			int interactableId,
			int authorId,
			string content
		)
		{
			InteractableId = interactableId;
			AuthorId = authorId;
			Content = content;
		}
	}
}