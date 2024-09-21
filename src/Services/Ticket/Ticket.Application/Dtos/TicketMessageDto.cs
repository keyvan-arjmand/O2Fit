
using Ticket.Domain.Enums;

namespace Ticket.Application.Dtos;

public class TicketMessageDto
{
    public string Message { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public bool IsUserRead { get; set; }
    public bool IsAdminRead { get; set; }
}