namespace Ticket.Application.Messages.V1.Commands.SoftDeleteUserMessage;

public record SoftDeleteUserMessageCommand(string Id) : IRequest;