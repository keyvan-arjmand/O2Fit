namespace Chat.Application.Messages.V1.Commands.CreateMessage;

public record CreateMessageCommand(string GroupId, string SenderUserId, string SenderFullName, string RecipientUserId, string RecipientFullName, string Content) : IRequest;