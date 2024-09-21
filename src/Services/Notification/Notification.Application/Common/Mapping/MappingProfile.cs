namespace Notification.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PhoneBook, PhoneBookDto>().ReverseMap();
        CreateMap<MessageLog, MessageLogDto>().ReverseMap();
    }
}