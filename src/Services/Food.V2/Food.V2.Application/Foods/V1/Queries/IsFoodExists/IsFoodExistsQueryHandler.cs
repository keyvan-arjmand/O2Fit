namespace Food.V2.Application.Foods.V1.Queries.IsFoodExists;

public class IsFoodExistsQueryHandler : IRequestHandler<IsFoodExistsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsFoodExistsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsFoodExistsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}