namespace Identity.V2.Application.Users.V1.Commands.UpdateStateIdAndCityId;

public class UpdateStateIdAndCityIdCommandHandler : IRequestHandler<UpdateStateIdAndCityIdCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateStateIdAndCityIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateStateIdAndCityIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);

        user.StateId = ObjectId.Parse(request.StateId);
        user.CityId = ObjectId.Parse(request.CityId);
        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                x => x.StateId,
                x => x.CityId
            }, null, cancellationToken);
    }
}