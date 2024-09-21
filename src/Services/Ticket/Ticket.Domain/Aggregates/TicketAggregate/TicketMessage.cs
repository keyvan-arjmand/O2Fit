using Ticket.Domain.Common;
using Ticket.Domain.Enums;

namespace Ticket.Domain.Aggregates.TicketAggregate;

public class TicketMessage : BaseAuditableEntity
{
    public string Message { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public bool IsUserRead { get; set; }
    public bool IsAdminRead { get; set; }
    public ObjectId? TicketId { get; set; } //Ticket parent

    //Auditable Create&Update // UserType Admin&User
}