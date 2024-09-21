namespace Food.V2.Application.Nationalities.V1.Commands.CreateNationality;

public class CreateNationalityCommandValidator : AbstractValidator<CreateNationalityCommand>
{
    public CreateNationalityCommandValidator()
    {
        RuleFor(x => x.Translation).NotNull().WithMessage("Translation Cannot be null");
    }
}