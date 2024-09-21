using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Queries.IsDietCategoryExits;

public class IsDietCategoryExitsQueryHandler : IRequestHandler<IsDietCategoryExitsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsDietCategoryExitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsDietCategoryExitsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<DietCategory>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}