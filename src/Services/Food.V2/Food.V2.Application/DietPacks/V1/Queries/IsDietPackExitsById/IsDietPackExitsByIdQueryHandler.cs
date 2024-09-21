namespace Food.V2.Application.DietPacks.V1.Queries.IsDietPackExitsById;

public class IsDietPackExitsByIdQueryHandler : IRequestHandler<IsDietPackExitsByIdQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsDietPackExitsByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsDietPackExitsByIdQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<DietPack>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}