using Ticket.Domain.Enums;

namespace Ticket.Application.UserContacts.V1.Commands.InsertContact;

public class InsertContactCommand:IRequest
{
    public string Message { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string Title { get; set; } = string.Empty;
}