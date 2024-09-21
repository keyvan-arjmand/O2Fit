namespace Food.V2.Application.MeasureUnits.V1.Commands.UpdateMeasureUnit;

public record UpdateMeasureUnitCommand(string Id, decimal Value, bool IsActive, CreateUpdateMeasureUnitTranslationDto Translation): IRequest;