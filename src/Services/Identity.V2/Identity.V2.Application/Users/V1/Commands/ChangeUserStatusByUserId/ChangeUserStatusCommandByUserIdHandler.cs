namespace Identity.V2.Application.Users.V1.Commands.ChangeUserStatusByUserId;

public class ChangeUserStatusCommandByUserIdHandler : IRequestHandler<ChangeUserStatusByUserIdCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeUserStatusCommandByUserIdHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ChangeUserStatusByUserIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>().GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);

        user.Status = request.Status;
        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                u => u.Status
            }, null, cancellationToken);
    }
}