using AutoMapper;
using Domain;
using Service.Models;

namespace WebFramework.CustomMapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserConfirmDto>()
                .ForMember(x=>x.ExpireTime, o=>o.MapFrom(e=>e.ConfirmCodeExpireTime));
        }
    }
}