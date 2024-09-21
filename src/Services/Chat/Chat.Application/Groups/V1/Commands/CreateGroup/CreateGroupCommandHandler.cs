namespace Chat.Application.Groups.V1.Commands.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateGroupCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var isDuplicate = await _uow.GenericRepository<Group>().AnyAsync(x => x.Name == request.GroupName, cancellationToken);
        
        if (isDuplicate)
            throw new BadRequestException("Duplicate group name");
        
        var group = new Group(request.GroupName, request.UserId, request.UserFullName, request.NutritionistId, request.NutritionistFullName);
        await _uow.GenericRepository<Group>().InsertOneAsync(group, null, cancellationToken);
    }
}