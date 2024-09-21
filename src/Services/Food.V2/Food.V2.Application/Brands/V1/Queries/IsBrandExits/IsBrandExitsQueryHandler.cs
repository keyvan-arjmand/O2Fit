using Food.V2.Domain.Aggregates.BrandAggregate;

namespace Food.V2.Application.Brands.V1.Queries.IsBrandExits;

public class IsBrandExitsQueryHandler : IRequestHandler<IsBrandExitsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsBrandExitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsBrandExitsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<Brand>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}