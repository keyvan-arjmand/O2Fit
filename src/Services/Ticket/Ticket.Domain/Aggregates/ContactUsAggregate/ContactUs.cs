using Ticket.Domain.Common;
using Ticket.Domain.Enums;

namespace Ticket.Domain.Aggregates.ContactUsAggregate;

public class ContactUs : AggregateRoot
{
    public string Message { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string Title { get; set; } = string.Empty;
}