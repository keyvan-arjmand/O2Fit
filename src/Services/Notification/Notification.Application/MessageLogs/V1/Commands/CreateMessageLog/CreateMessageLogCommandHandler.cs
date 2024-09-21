namespace Notification.Application.MessageLogs.V1.Commands.CreateMessageLog;

public class CreateMessageLogCommandHandler : IRequestHandler<CreateMessageLogCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateMessageLogCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateMessageLogCommand request, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLog(request.Text ,request.ToFcmToken, request.ToPhoneNumber);
        await _uow.GenericRepository<MessageLog>().InsertOneAsync(messageLog, null, cancellationToken);
    }
}