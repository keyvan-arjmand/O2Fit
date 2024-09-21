namespace Food.V2.Application.MeasureUnits.V1.Commands.CreateMeasureUnit;

public class CreateMeasureUnitCommandValidator : AbstractValidator<CreateMeasureUnitCommand>
{
    public CreateMeasureUnitCommandValidator()
    {
        RuleFor(x => x.Value).NotEmpty().WithMessage("Value can not be empty")
            .NotNull().WithMessage("Value can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Value should greater than or equal to zero");

        RuleFor(x => x.Translation.Persian).NotEmpty().WithMessage("TranslationDto.Persian can not be empty")
            .NotNull().WithMessage("TranslationDto.Persian can not be null");
    }
}