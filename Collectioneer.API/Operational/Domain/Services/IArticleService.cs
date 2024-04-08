using Collectioneer.API.Operational.Domain.Commands;

namespace Collectioneer.API.Operational.Domain.Services
{
    public interface IArticleService
    {
        public Task GenerateCollectibleArticle(ArticleCreationCommand collectible);
    }
}
