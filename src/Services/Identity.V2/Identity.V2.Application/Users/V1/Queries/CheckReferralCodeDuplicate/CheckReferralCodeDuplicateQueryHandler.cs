namespace Identity.V2.Application.Users.V1.Queries.CheckReferralCodeDuplicate;

public class CheckReferralCodeDuplicateQueryHandler: IRequestHandler<CheckReferralCodeDuplicateQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public CheckReferralCodeDuplicateQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<bool> Handle(CheckReferralCodeDuplicateQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.UserGenericRepository<User>().AnyAsync(x => x.ReferralCode == request.Code, cancellationToken);
        return result;
    }
}