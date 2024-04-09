using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Application.Services.Internal.MappingProfiles
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<BidCreationCommand, Bid>();
        }
    }
}
