namespace Chat.Application.ChatFiles.V1.Commands.CreateChatFile;

public record CreateChatFileCommand(string FileName, string FileUrl) : IRequest;