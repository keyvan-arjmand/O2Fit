using AutoMapper;
using Blogging.Domain.Entities.Blogs;
using Blogging.Domain.Entities.Translation;
using Blogging.Service.Dtos;

namespace Blogging.WebFramework.CustomMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Translation, TranslationDto>().ReverseMap();
            CreateMap<BlogDto, Blog>().ReverseMap();
        }
    }
}