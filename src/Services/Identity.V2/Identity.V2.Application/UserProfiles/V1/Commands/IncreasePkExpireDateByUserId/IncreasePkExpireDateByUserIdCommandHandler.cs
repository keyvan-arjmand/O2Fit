namespace Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkExpireDateByUserId;

public class IncreasePkExpireDateByUserIdCommandHandler : IRequestHandler<IncreasePkExpireDateByUserIdCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreasePkExpireDateByUserIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreasePkExpireDateByUserIdCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq(x => x.Id, ObjectId.Parse(request.UserId));
        var user = await _uow.UserGenericRepository<User>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);
        
        if (user.UserProfile.PkExpireDate == null)
        {
            user.UserProfile.PkExpireDate = request.PkExpireDate;
        }
        else
        {
            var days = request.PkExpireDate.Day;
            user.UserProfile.PkExpireDate.Value.AddDays(days);
        }
        
        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                u => u.UserProfile.PkExpireDate
            }, null, cancellationToken);
    }
}