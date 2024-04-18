using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Abstracts;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
	public class Review : ITimestamped
	{
		public int Id { get; set; }
		public int ReviewerId { get; set; }
		public int ArticleId { get; set; }
		public string Content { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Reviewer { get; set; }
		public Article? ReviewedArticle { get; set; }

		public Review(
			int reviewerId,
			int articleId,
			string content,
			int rating
		)
		{
			ReviewerId = reviewerId;
			ArticleId = articleId;
			Content = content;
			Rating = rating;
		}
	}

}

