using Collectioneer.API.Operational.Domain.Models.Exceptions;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public int CollectibleId { get; set; }
        public Collectible Collectible { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }

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
