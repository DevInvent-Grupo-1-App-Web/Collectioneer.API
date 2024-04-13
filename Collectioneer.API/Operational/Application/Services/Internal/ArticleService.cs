using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Application.Services.Internal
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateCollectibleArticle(ArticleCreationCommand collectible)
        {
            var article = new Article
            (
                collectible.CollectibleId, 
                collectible.Title 
            );

            await _articleRepository.Add(article);

            await _unitOfWork.CompleteAsync();
        }
    }
}
