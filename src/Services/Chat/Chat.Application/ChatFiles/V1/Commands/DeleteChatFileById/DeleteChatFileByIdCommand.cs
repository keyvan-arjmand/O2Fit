namespace Chat.Application.ChatFiles.V1.Commands.DeleteChatFileById;

public record DeleteChatFileByIdCommand(string Id) : IRequest;