using Collectioneer.API.Operational.Domain.Commands;

namespace Collectioneer.API.Operational.Domain.Services.Intern
{
    public interface IArticleService
    {
        public Task GenerateCollectibleArticle(ArticleCreationCommand collectible);
    }
}
