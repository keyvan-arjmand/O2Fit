namespace Identity.V2.Application.SpecialDiseases.V1.Commands.SoftDeleteSpecialDisease;

public class SoftDeleteSpecialDiseaseCommandValidator : AbstractValidator<SoftDeleteSpecialDiseaseCommand>
{
    public SoftDeleteSpecialDiseaseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");

    }
}