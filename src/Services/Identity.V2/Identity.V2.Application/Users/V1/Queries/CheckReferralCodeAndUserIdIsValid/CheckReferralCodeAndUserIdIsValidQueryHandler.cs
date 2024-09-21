namespace Identity.V2.Application.Users.V1.Queries.CheckReferralCodeAndUserIdIsValid;

public class CheckReferralCodeAndUserIdIsValidQueryHandler : IRequestHandler<CheckReferralCodeAndUserIdIsValidQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public CheckReferralCodeAndUserIdIsValidQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(CheckReferralCodeAndUserIdIsValidQuery request, CancellationToken cancellationToken)
    {
        return _uow.UserGenericRepository<User>()
            .AnyAsync(x => x.ReferralCode == request.Code && x.Id == ObjectId.Parse(request.UserId), cancellationToken);
    }
}