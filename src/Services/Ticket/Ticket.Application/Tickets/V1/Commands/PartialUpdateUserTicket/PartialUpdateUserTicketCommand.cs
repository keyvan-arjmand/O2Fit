using Ticket.Application.Dtos;

namespace Ticket.Application.Tickets.V1.Commands.PartialUpdateUserTicket;

public class PartialUpdateUserTicketCommand : IRequest
{
    public string TicketId { get; set; } = string.Empty;
    public TicketMessageDto TicketMessage { get; set; } = new();
}