namespace Food.V2.Application.MeasureUnits.V1.Commands.CreateMeasureUnit;

public record CreateMeasureUnitCommand(decimal Value, bool IsActive, CreateUpdateMeasureUnitTranslationDto Translation) : IRequest<string>;