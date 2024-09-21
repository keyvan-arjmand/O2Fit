namespace Chat.Application.Groups.V1.Commands.AddConnectionToGroup;

public class AddConnectionToGroupCommandHandler : IRequestHandler<AddConnectionToGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public AddConnectionToGroupCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddConnectionToGroupCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Group>.Filter.Eq(x => x.Name, request.GroupName);
        var group = await _uow.GenericRepository<Group>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
        
        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupName);
        
        group.Connections.Add(request.Connection.ToEntity<Connection>());

        await _uow.GenericRepository<Group>().UpdateOneAsync(x => x.Name == request.GroupName, group,
            new Expression<Func<Group, object>>[]
            {
                x => x.Connections
            }, null, cancellationToken);
    }
}