namespace Chat.Application.Messages.V1.Commands.ReadAllUnReadMessages;

public class ReadAllUnReadMessagesCommandHandler : IRequestHandler<ReadAllUnReadMessagesCommand>
{
    private readonly IUnitOfWork _uow;

    public ReadAllUnReadMessagesCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ReadAllUnReadMessagesCommand request, CancellationToken cancellationToken)
    {
        var group = await _uow.GenericRepository<Group>().GetByIdAsync(request.GroupId, cancellationToken);
        if (group == null)
            throw new NotFoundException(nameof(request.GroupId), request.GroupId);

        if (group.Messages.Count >= 0)
        {
            foreach (var groupMessage in group.Messages.Where(x =>
                         x.RecipientUserId == ObjectId.Parse(request.CurrentUserId) &&
                         x.SenderUserId == ObjectId.Parse(request.RecipientId) && x.DateRead == null))
            {
                groupMessage.DateRead = DateTime.UtcNow;
                groupMessage.Status = MessageStatus.Read;
                await _uow.GenericRepository<Group>().UpdateOneAsync(x => x.Id == request.GroupId, group,
                    new Expression<Func<Group, object>>[]
                    {
                        x => x.Messages
                    }, null, cancellationToken);
            }
        }

        //var filter = Builders<Message>.Filter.Eq(x => x.SenderUserId, ObjectId.Parse(request.RecipientId));
        //filter &= Builders<Message>.Filter.Eq(x => x.RecipientUserId, ObjectId.Parse(request.CurrentUserId));
        //filter &= Builders<Message>.Filter.Eq(x => x.DateRead, null);
        //
        //var unreadMessages = await _uow.GenericRepository<Message>()
        //    .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        //
        //foreach (var unreadMessage in unreadMessages)
        //{
        //    unreadMessage.DateRead = DateTime.UtcNow;
        //    unreadMessage.Status = MessageStatus.Read;
        //    await _uow.GenericRepository<Message>().UpdateOneAsync(x => x.Id == unreadMessage.Id, unreadMessage,
        //        new Expression<Func<Message, object>>[]
        //        {
        //            x => x.DateRead,
        //            x=>x.Status
        //        }, null, cancellationToken);
        //}
    }
}