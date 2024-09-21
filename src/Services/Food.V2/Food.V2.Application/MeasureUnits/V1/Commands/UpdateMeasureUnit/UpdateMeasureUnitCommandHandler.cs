namespace Food.V2.Application.MeasureUnits.V1.Commands.UpdateMeasureUnit;

public class UpdateMeasureUnitCommandHandler : IRequestHandler<UpdateMeasureUnitCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateMeasureUnitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateMeasureUnitCommand request, CancellationToken cancellationToken)
    {
        var measureUnit = await _uow.GenericRepository<MeasureUnit>().GetByIdAsync(request.Id, cancellationToken);

        if (measureUnit == null)
            throw new NotFoundException(nameof(MeasureUnit), request.Id);
        
        measureUnit.IsActive = request.IsActive;
        measureUnit.Value = new NotNegativeForDecimalTypes(request.Value);
        measureUnit.Translation.Arabic = request.Translation.Arabic;
        measureUnit.Translation.English = request.Translation.English;
        measureUnit.Translation.Persian = request.Translation.Persian;
        await _uow.GenericRepository<MeasureUnit>().UpdateOneAsync(x => x.Id == request.Id, measureUnit,
            new Expression<Func<MeasureUnit, object>>[]
            {
                x => x.Translation.Arabic,
                x => x.Translation.English,
                x => x.Translation.Persian,
                x => x.IsActive,
                x => x.Value
            },null,cancellationToken);
    }
}