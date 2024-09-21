namespace Identity.V2.Application.Users.V1.Queries.CheckConfirmCodeIsNotDuplicate;

public class CheckConfirmCodeIsNotDuplicateQueryHandler: IRequestHandler<CheckConfirmCodeIsNotDuplicateQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public CheckConfirmCodeIsNotDuplicateQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(CheckConfirmCodeIsNotDuplicateQuery request, CancellationToken cancellationToken)
    {
        var result = _uow.UserGenericRepository<User>().AnyAsync(x => x.ConfirmCode == request.Code, cancellationToken);
        return result;
    }
}