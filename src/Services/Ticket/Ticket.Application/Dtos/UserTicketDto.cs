using Ticket.Domain.Enums;

namespace Ticket.Application.Dtos;

public class UserTicketDto
{
    public string Title { get; set; } = string.Empty;
    public Classification Classification { get; set; }
    public List<TicketMessageDto> TicketMessages { get; set; } = new();
    public bool IsEnd { get; set; }
}