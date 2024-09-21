namespace Food.V2.Application.MeasureUnits.V1.Commands.UpdateMeasureUnit;

public class UpdateMeasureUnitCommandValidator : AbstractValidator<UpdateMeasureUnitCommand>
{
    public UpdateMeasureUnitCommandValidator()
    {
        RuleFor(x => x.Value).NotEmpty().WithMessage("Value can not be empty")
            .NotNull().WithMessage("Value can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Value should greater than or equal to zero");

        RuleFor(x => x.Translation.Persian).NotEmpty().WithMessage("TranslationDto.Persian can not be empty")
            .NotNull().WithMessage("TranslationDto.Persian can not be null");
        
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");

    }
}