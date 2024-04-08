using Collectioneer.API.Social.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
    public class Collectible
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public User Owner { get; set; } 
        public float? Value { get; set; }
        public int CommunityId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public Collectible()
        {
            Article = new Article
            {
                Title = Name,
                Content = "This is a new article.\nCustomize it to your liking!"
            };
        }
    }
}
