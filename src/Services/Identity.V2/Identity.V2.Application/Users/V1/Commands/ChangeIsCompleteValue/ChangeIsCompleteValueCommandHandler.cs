namespace Identity.V2.Application.Users.V1.Commands.ChangeIsCompleteValue;

public class ChangeIsCompleteValueCommandHandler: IRequestHandler<ChangeIsCompleteValueCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeIsCompleteValueCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ChangeIsCompleteValueCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(user), request.UserId);
        user.IsComplete = request.IsComplete;

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                x => x.IsComplete
            },null,cancellationToken);
    }
}