namespace Identity.V2.Application.Users.V1.Queries.IsUserReferralCodeValid;

public class IsUserReferralCodeValidQueryHandler : IRequestHandler<IsUserReferralCodeValidQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsUserReferralCodeValidQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public  Task<bool> Handle(IsUserReferralCodeValidQuery request, CancellationToken cancellationToken)
    {
        return _uow.UserGenericRepository<User>()
            .AnyAsync(x => x.ReferralCode == request.ReferralCode.ToUpper(), cancellationToken);
    }
}