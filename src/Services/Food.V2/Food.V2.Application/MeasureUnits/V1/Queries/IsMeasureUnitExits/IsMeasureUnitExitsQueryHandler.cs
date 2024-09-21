namespace Food.V2.Application.MeasureUnits.V1.Queries.IsMeasureUnitExits;

public class IsMeasureUnitExitsQueryHandler : IRequestHandler<IsMeasureUnitExitsQuery,bool>
{
    private readonly IUnitOfWork _uow;

    public IsMeasureUnitExitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsMeasureUnitExitsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<MeasureUnit>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}