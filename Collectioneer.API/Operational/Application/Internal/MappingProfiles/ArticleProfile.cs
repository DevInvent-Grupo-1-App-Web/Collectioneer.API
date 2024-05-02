using AutoMapper;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Application.Internal.MappingProfiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleCreationCommand, Article>();
        }
    }
}
