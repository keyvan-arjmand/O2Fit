using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Queries.IsNationalityExits;

public class IsNationalityExitsQueryHandler : IRequestHandler<IsNationalityExitsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsNationalityExitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsNationalityExitsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<Nationality>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}