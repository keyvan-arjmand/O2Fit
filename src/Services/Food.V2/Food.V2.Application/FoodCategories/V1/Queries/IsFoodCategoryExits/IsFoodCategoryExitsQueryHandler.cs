using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Queries.IsFoodCategoryExits;

public class IsFoodCategoryExitsQueryHandler : IRequestHandler<IsFoodCategoryExitsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsFoodCategoryExitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsFoodCategoryExitsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<Category>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}