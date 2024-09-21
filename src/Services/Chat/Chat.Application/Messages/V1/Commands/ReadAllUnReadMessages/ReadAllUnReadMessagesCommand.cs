namespace Chat.Application.Messages.V1.Commands.ReadAllUnReadMessages;

public record ReadAllUnReadMessagesCommand(string GroupId, string CurrentUserId, string RecipientId) : IRequest;