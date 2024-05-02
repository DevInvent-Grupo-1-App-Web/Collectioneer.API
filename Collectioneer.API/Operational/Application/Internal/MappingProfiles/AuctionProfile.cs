using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Application.Internal.MappingProfiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<AuctionCreationCommand, Auction>();
        }
    }
}
