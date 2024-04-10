using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Application.Services.Internal
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateCollectibleArticle(ArticleCreationCommand collectible)
        {
            var article = _mapper.Map<Article>(collectible);

            await _articleRepository.Add(article);
            await _unitOfWork.CompleteAsync();
        }
    }
}
