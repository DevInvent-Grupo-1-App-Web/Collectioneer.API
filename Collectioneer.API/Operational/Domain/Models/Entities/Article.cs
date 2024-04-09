namespace Collectioneer.API.Operational.Domain.Models.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public int CollectibleId { get; set; }
        public Collectible Collectible { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
