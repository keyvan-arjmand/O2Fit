namespace Chat.Application.Groups.V1.Commands.CreateGroupWithConnection;

public class CreateGroupWithConnectionCommandHandler : IRequestHandler<CreateGroupWithConnectionCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateGroupWithConnectionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateGroupWithConnectionCommand request, CancellationToken cancellationToken)
    {
        if (request.Connections.Count <= 0)
            throw new BadRequestException("Connection list is empty");

        var isDuplicate = await _uow.GenericRepository<Group>().AnyAsync(x => x.Name == request.GroupName, cancellationToken);
        
        if (isDuplicate)
            throw new BadRequestException("Duplicate group name");
        
        var connections = request.Connections.ToEntity<Connection>().ToList();
        var group = new Group(request.GroupName, connections);

        await _uow.GenericRepository<Group>().InsertOneAsync(group, null, cancellationToken);
    }
}