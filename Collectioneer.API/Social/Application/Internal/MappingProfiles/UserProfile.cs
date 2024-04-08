using AutoMapper;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Entities;

namespace Collectioneer.API.Social.Application.Internal.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterCommand, User>();
        }
    }
}
