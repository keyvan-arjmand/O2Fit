using MongoDB.Driver.Linq;

namespace Chat.Application.Groups.V1.Commands.RemoveConnectionFromGroup;

public class RemoveConnectionFromGroupCommandHandler : IRequestHandler<RemoveConnectionFromGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public RemoveConnectionFromGroupCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(RemoveConnectionFromGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _uow.GenericRepository<Group>().GetByIdAsync(request.GroupId, cancellationToken);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);
        var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == request.ConnectionId);
        if (connection == null)
            throw new NotFoundException(nameof(Connection), request.ConnectionId);

        group.Connections.Remove(connection);

        await _uow.GenericRepository<Group>().UpdateOneAsync(x => x.Id == request.GroupId, group,
            new Expression<Func<Group, object>>[]
            {
                x => x.Connections
            }, null, cancellationToken);
    }
}