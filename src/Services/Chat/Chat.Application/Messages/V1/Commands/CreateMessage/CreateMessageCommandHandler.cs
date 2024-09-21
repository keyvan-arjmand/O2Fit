namespace Chat.Application.Messages.V1.Commands.CreateMessage;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateMessageCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var group = await _uow.GenericRepository<Group>().GetByIdAsync(request.GroupId, cancellationToken);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);
        
        var message = new Message(ObjectId.Parse(request.SenderUserId), request.SenderFullName,
            ObjectId.Parse(request.RecipientUserId), request.RecipientFullName,
            request.Content);

        group.Messages.Add(message);

        await _uow.GenericRepository<Group>().UpdateOneAsync(x => x.Id == request.GroupId, group,
            new Expression<Func<Group, object>>[]
            {
                x => x.Messages
            }, null, cancellationToken);
    }
}