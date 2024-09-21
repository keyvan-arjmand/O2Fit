using Ticket.Domain.Common;
using Ticket.Domain.Enums;

namespace Ticket.Domain.Aggregates.TicketAggregate;

public class Ticket : AggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public Classification Classification { get; set; }
    public List<TicketMessage> TicketMessages { get; set; } = new();
    public bool IsEnd { get; set; }
}