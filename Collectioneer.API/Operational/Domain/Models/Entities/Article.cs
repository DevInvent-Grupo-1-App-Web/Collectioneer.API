using Collectioneer.API.Operational.Domain.Interfaces;
using Collectioneer.API.Operational.Domain.Models.Exceptions;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
	public class Article : IReviewable, IMediaHolder, ITimestamped
	{
		public int Id { get; set; }
		public int CollectibleId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public Collectible? Collectible { get; set; }
		public ICollection<Review> Reviews { get; set;} = [];
		public ICollection<MediaElement> MediaElements { get; set; } = [];

		public Article(int collectibleId, string title)
		{
			CollectibleId = collectibleId;
			SetTitle(title);
		}

		private void SetTitle(string title)
		{
			if (title.Length < 3 || title.Length > 50)
			{
				throw new ArticleModelException("Title must be between 3 and 50 characters long.");
			}

			Title = title;
		}
		public void SetContent(string content)
		{
			Content = content;
		}
	}
}
