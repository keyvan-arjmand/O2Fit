namespace Notification.Application.MessageLogs.V1.Commands.RemoveMessageLog;

public class RemoveMessageLogCommandHandler : IRequestHandler<RemoveMessageLogCommand>
{
    private readonly IUnitOfWork _uow;

    public RemoveMessageLogCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(RemoveMessageLogCommand request, CancellationToken cancellationToken)
    {
        var messageLog = await _uow.GenericRepository<MessageLog>().GetByIdAsync(request.Id, cancellationToken);
        if (messageLog == null)
            throw new NotFoundException(nameof(MessageLog), request.Id);

        await _uow.GenericRepository<MessageLog>()
            .DeleteOneAsync(x => x.Id == request.Id, messageLog, null, cancellationToken);
    }
}