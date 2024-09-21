using Ticket.Domain.Common;
using Ticket.Domain.Enums;

namespace Ticket.Domain.Aggregates.MessageAggregate;

public class Message : AggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageName { get; set; }
    public string Link { get; set; } = string.Empty;
    public LinkTarget Type { get; set; }
    public ObjectId? UserId { get; set; } // Message To User
    public Classification Classification { get; set; }
    public UserType UserType { get; set; }
    public bool? IsUserRead { get; set; }
    public bool IsGeneral { get; set; }
    public bool IsForce { get; set; }
}