using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Application.Services.Internal
{
    public class CollectibleService : ICollectibleService
    {
        private readonly ICollectibleRepository _collectibleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CollectibleService(ICollectibleRepository collectibleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _collectibleRepository = collectibleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> RegisterCollectible(CollectibleRegisterCommand command)
        {
            var collectible = _mapper.Map<Collectible>(command);

            await _collectibleRepository.Add(collectible);
            await _unitOfWork.CompleteAsync();

            return collectible.Id;
        }
    }
}
