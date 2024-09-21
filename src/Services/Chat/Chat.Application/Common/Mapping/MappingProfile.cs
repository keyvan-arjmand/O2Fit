namespace Chat.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Connection, ConnectionDto>().ReverseMap();
        CreateMap<Group, GetGroupByIdDto>().ReverseMap();
        CreateMap<Group, GetGroupPaginatedDto>().ReverseMap();
        CreateMap<Message, MessageDto>().ReverseMap();
        
    }
}