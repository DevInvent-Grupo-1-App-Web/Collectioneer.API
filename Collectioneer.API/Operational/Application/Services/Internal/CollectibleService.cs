using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;

namespace Collectioneer.API.Operational.Application.Services.Internal
{
	public class CollectibleService(
			ICollectibleRepository collectibleRepository,
			IMapper mapper,
			IUnitOfWork unitOfWork,
			IArticleService articleService,
			IUserService userService) : ICollectibleService
	{
		private readonly ICollectibleRepository _collectibleRepository = collectibleRepository;
		private readonly IArticleService _articleService = articleService;
		private readonly IUserService _userService = userService;
		private readonly IMapper _mapper = mapper;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<int> RegisterCollectible(CollectibleRegisterCommand command)
		{
			var collectible = new Collectible(
					command.CommunityId,
					command.Name,
					command.OwnerId,
					command.Value
			);

			await _collectibleRepository.Add(collectible);
			await _unitOfWork.CompleteAsync();


			await _articleService.GenerateCollectibleArticle(new ArticleCreationCommand
			(
					collectible.Name,
					collectible.Id));

			await _unitOfWork.CompleteAsync();

			return collectible.Id;
		}

		public async Task RegisterAuctionIdInCollectible(CollectibleAuctionIdRegisterCommand command)
		{
			var collectible = await _collectibleRepository.GetById(command.CollectibleId) ?? throw new Exception("Collectible not found.");
			collectible.AuctionId = command.AuctionId;
			await _collectibleRepository.Update(collectible);
			await _unitOfWork.CompleteAsync();
		}
	}
}
