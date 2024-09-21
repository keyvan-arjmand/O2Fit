namespace Food.V2.Application.MeasureUnits.V1.Queries.GetAllMeasureUnits;

public class GetAllMeasureUnitsQueryHandler : IRequestHandler<GetAllMeasureUnitsQuery, List<MeasureUnitDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllMeasureUnitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<MeasureUnitDto>> Handle(GetAllMeasureUnitsQuery request, CancellationToken cancellationToken)
    {
        var allMeasureUnits = await _uow.GenericRepository<MeasureUnit>().GetAllAsync(cancellationToken);
        return allMeasureUnits.ToDto<MeasureUnitDto>().ToList();
    }
}