namespace Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkDietDateByUserId;

public class IncreasePkDietDateByUserIdCommandHandler : IRequestHandler<IncreasePkDietDateByUserIdCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreasePkDietDateByUserIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreasePkDietDateByUserIdCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq(x => x.Id, ObjectId.Parse(request.UserId));
        var user = await _uow.UserGenericRepository<User>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        
        if (user == null)
            throw new NotFoundException(nameof(UserProfile), request.UserId);
        
        if (user.UserProfile.DietPkExpireDate == null)
        {
            user.UserProfile.DietPkExpireDate = request.DietExpireDate;
        }
        else
        {
            var days = request.DietExpireDate.Day;
            user.UserProfile.DietPkExpireDate.Value.AddDays(days);
        }
        
        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId) , user,
            new Expression<Func<User, object>>[]
            {
                u => u.UserProfile.DietPkExpireDate
            }, null, cancellationToken);
    }
}