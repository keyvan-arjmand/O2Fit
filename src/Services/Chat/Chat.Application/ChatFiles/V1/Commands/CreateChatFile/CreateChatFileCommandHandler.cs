namespace Chat.Application.ChatFiles.V1.Commands.CreateChatFile;

public class CreateChatFileCommandHandler : IRequestHandler<CreateChatFileCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateChatFileCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task Handle(CreateChatFileCommand request, CancellationToken cancellationToken)
    {
        var chatFile = new ChatFile(request.FileName, request.FileUrl);
        return _uow.GenericRepository<ChatFile>().InsertOneAsync(chatFile, null, cancellationToken);
    }
}