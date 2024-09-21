namespace Chat.Application.Groups.V1.Commands.DeleteGroup;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteGroupCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _uow.GenericRepository<Group>().GetByIdAsync(request.Id, cancellationToken);
        
        if (group == null)
            throw new NotFoundException(nameof(Group), request.Id);

        await _uow.GenericRepository<Group>().DeleteOneAsync(x => x.Id == request.Id, group, null, cancellationToken);
    }
}