using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Application.External
{
	public class PostDTO {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public int AuthorId { get; set; }

		public PostDTO(Post post) {
			Id = post.Id;
			Title = post.Title;
			Content = post.Content;
			CreatedAt = post.CreatedAt;
			UpdatedAt = post.UpdatedAt;
			AuthorId = post.AuthorId;
		}
	}
}