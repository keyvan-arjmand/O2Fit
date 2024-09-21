namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateFastingMode;

public class UpdateFastingModeCommandHandler : IRequestHandler<UpdateFastingModeCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateFastingModeCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateFastingModeCommand request, CancellationToken cancellationToken)
    {
        //var filter = Builders<UserProfile>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId));
        if (user == null)
            throw new NotFoundException(nameof(UserProfile), request.UserId);
       
        user.UserProfile.FastingMode = request.FastingMode;

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new System.Linq.Expressions.Expression<System.Func<User, object>>[]
            {
                x => x.UserProfile.FastingMode
            }, null, cancellationToken);
    }
}