namespace Identity.V2.Application.SpecialDiseases.V1.Commands.CreateSpecialDisease;

public class CreateSpecialDiseaseCommandValidator : AbstractValidator<CreateSpecialDiseaseCommand>
{
    public CreateSpecialDiseaseCommandValidator()
    {
        RuleFor(x => x.Name.Arabic).NotEmpty().WithMessage("Arabic can not be empty")
            .NotNull().WithMessage("Arabic can not be null");
        RuleFor(x => x.Name.English).NotEmpty().WithMessage("English can not be empty")
            .NotNull().WithMessage("English can not be null");
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian can not be empty")
            .NotNull().WithMessage("Persian can not be null");

    }
}