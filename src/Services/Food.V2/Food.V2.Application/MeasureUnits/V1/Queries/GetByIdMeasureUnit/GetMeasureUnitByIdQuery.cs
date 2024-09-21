namespace Food.V2.Application.MeasureUnits.V1.Queries.GetByIdMeasureUnit;

public record GetMeasureUnitByIdQuery(string Id): IRequest<MeasureUnitDto>;