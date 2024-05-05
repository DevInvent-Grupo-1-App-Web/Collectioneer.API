using AutoMapper;
using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;

namespace Collectioneer.API.Operational.Application.Internal.Services
{
	public class CollectibleService(
			ICollectibleRepository collectibleRepository,
			IUnitOfWork unitOfWork) : ICollectibleService
	{
		private readonly ICollectibleRepository _collectibleRepository = collectibleRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<CollectibleDTO> RegisterCollectible(CollectibleRegisterCommand command)
		{
			var collectible = new Collectible(
					command.CommunityId,
					command.Name,
					command.Description,
					command.UserId,
					command.Value
			);

			await _collectibleRepository.Add(collectible);
			await _unitOfWork.CompleteAsync();

			return new CollectibleDTO(collectible);
		}

		public async Task RegisterAuctionIdInCollectible(CollectibleAuctionIdRegisterCommand command)
		{
			var collectible = await _collectibleRepository.GetById(command.CollectibleId) ?? throw new Exception("Collectible not found.");
			collectible.AuctionId = command.AuctionId;
			await _collectibleRepository.Update(collectible);
			await _unitOfWork.CompleteAsync();
		}

		public async Task<CollectibleDTO> GetCollectible(int id)
		{
			var collectible = await _collectibleRepository.GetById(id) ?? throw new Exception("Collectible not found.");

			return new CollectibleDTO(collectible);
		}

		public async Task<ICollection<CollectibleDTO>> GetCollectibles(CollectibleBulkRetrieveQuery command)
		{
			var collectibles = await _collectibleRepository.GetCollectibles(command.CommunityId, command.MaxAmount, command.Offset);
			return collectibles.Select(c => new CollectibleDTO(c)).ToList();
		}
	}
}
