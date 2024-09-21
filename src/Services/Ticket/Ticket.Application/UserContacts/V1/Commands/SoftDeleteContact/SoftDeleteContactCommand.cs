namespace Ticket.Application.UserContacts.V1.Commands.SoftDeleteContact;

public record SoftDeleteContactCommand(string Id) : IRequest;