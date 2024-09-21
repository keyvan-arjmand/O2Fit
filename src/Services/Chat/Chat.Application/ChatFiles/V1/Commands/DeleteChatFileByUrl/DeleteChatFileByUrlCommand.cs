namespace Chat.Application.ChatFiles.V1.Commands.DeleteChatFileByUrl;

public record DeleteChatFileByUrlCommand(string Url) : IRequest;