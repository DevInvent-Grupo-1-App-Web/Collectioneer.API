using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Application.Internal.MappingProfiles
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<BidCreationCommand, Bid>();
        }
    }
}
