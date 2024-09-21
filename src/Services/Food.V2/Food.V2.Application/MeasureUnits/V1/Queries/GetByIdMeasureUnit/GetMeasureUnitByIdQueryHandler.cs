namespace Food.V2.Application.MeasureUnits.V1.Queries.GetByIdMeasureUnit;

public class GetMeasureUnitByIdQueryHandler : IRequestHandler<GetMeasureUnitByIdQuery, MeasureUnitDto>
{
    private readonly IUnitOfWork _uow;

    public GetMeasureUnitByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<MeasureUnitDto> Handle(GetMeasureUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var measureUnit = await _uow.GenericRepository<MeasureUnit>().GetByIdAsync(request.Id, cancellationToken);
        if (measureUnit == null)
            throw new NotFoundException(nameof(MeasureUnit), request.Id);

        return measureUnit.ToDto<MeasureUnitDto>();
    }
}