using Ticket.Domain.Enums;

namespace Ticket.Application.Tickets.V1.Commands.InsertUserTicket;

public class InsertUserTicketCommand:IRequest
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public Classification Classification { get; set; }
}