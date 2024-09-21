using Ticket.Application.Dtos;
using Ticket.Domain.Aggregates.MessageAggregate;
using Ticket.Domain.Aggregates.TicketAggregate;

namespace Ticket.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<TicketMessageDto, TicketMessage>().ReverseMap();
        CreateMap<UserTicketDto, Domain.Aggregates.TicketAggregate.Ticket>().ReverseMap();
    }
}