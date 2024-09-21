namespace Food.V2.Application.MeasureUnits.V1.Queries.IsMeasureUnitExits;

public class IsMeasureUnitExitsQueryValidator : AbstractValidator<IsMeasureUnitExitsQuery>
{
    public IsMeasureUnitExitsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}