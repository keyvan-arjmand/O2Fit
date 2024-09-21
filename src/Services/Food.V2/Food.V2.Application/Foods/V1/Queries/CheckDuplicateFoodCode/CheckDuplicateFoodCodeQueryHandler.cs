namespace Food.V2.Application.Foods.V1.Queries.CheckDuplicateFoodCode;

public class CheckDuplicateFoodCodeQueryHandler: IRequestHandler<CheckDuplicateFoodCodeQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public CheckDuplicateFoodCodeQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(CheckDuplicateFoodCodeQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .AnyAsync(x => x.FoodCode == request.FoodCode, cancellationToken);
    }
}