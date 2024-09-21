namespace Food.V2.Application.MeasureUnits.V1.Queries.GetByIdMeasureUnit;

public class GetMeasureUnitQueryByIdValidator: AbstractValidator<GetMeasureUnitByIdQuery>
{
    public GetMeasureUnitQueryByIdValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}