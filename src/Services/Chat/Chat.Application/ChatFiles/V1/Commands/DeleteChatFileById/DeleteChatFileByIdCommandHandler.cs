namespace Chat.Application.ChatFiles.V1.Commands.DeleteChatFileById;

public class DeleteChatFileByIdCommandHandler : IRequestHandler<DeleteChatFileByIdCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteChatFileByIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteChatFileByIdCommand request, CancellationToken cancellationToken)
    {
        var chatFile = await _uow.GenericRepository<ChatFile>().GetByIdAsync(request.Id, cancellationToken);
        if (chatFile == null)
            throw new NotFoundException(nameof(ChatFile), request.Id);

        await _uow.GenericRepository<ChatFile>()
            .DeleteOneAsync(x => x.Id == request.Id, chatFile, null, cancellationToken);
    }
}