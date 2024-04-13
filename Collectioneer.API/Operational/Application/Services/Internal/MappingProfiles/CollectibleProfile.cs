using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Application.Services.Internal.MappingProfiles
{
    public class CollectibleProfile : Profile
    {
        public CollectibleProfile()
        {
            CreateMap<CollectibleRegisterCommand, Collectible>();
        }
    }
}
