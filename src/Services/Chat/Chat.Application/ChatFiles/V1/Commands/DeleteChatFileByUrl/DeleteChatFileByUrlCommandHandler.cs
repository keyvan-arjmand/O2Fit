namespace Chat.Application.ChatFiles.V1.Commands.DeleteChatFileByUrl;

public class DeleteChatFileByUrlCommandHandler : IRequestHandler<DeleteChatFileByUrlCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public DeleteChatFileByUrlCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(DeleteChatFileByUrlCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<ChatFile>.Filter.Eq(x => x.FileUrl, request.Url);
        var chatFile = await _uow.GenericRepository<ChatFile>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (chatFile == null)
            throw new NotFoundException(nameof(ChatFile), request.Url);
        
        _fileService.RemoveFile(chatFile.FileName, PathConstants.ChatFilePath);
        await _uow.GenericRepository<ChatFile>()
            .DeleteOneAsync(x => x.Id == chatFile.Id, chatFile, null, cancellationToken);
    }
}