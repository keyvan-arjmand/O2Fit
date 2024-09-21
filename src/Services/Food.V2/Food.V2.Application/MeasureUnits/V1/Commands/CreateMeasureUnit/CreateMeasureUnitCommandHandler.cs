namespace Food.V2.Application.MeasureUnits.V1.Commands.CreateMeasureUnit;

public class CreateMeasureUnitCommandHandler : IRequestHandler<CreateMeasureUnitCommand, string>
{
    private readonly IUnitOfWork _uow;

    public CreateMeasureUnitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(CreateMeasureUnitCommand request, CancellationToken cancellationToken)
    {
        var translation = request.Translation.ToEntity<MeasureUnitTranslation>();
        var measureUnit = new MeasureUnit( new NotNegativeForDecimalTypes(request.Value) , request.IsActive, translation);
        await _uow.GenericRepository<MeasureUnit>().InsertOneAsync(measureUnit, null, cancellationToken);
        return measureUnit.Id;
    }
}