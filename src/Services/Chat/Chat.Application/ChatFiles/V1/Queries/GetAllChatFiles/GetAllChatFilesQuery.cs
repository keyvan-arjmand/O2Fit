namespace Chat.Application.ChatFiles.V1.Queries.GetAllChatFiles;

public record GetAllChatFilesQuery() : IRequest<List<ChatFileDto>>;