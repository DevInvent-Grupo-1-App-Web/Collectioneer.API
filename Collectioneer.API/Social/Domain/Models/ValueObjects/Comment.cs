using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Abstracts;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Comment : Interactable, ITimestamped
	{
		public int Id { get; set; }
		public int InteractableId { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsHidden { get; set; } = false;

		// Navigation properties
		public Interactable? Interactable { get; set; }
		public User? User { get; set; }

		public Comment(
			int interactableId,
			int userId,
			string content
		)
		{
			InteractableId = interactableId;
			UserId = userId;
			Content = content;
		}
	}
}