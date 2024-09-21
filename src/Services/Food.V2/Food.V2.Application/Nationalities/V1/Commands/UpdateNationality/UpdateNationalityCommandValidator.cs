namespace Food.V2.Application.Nationalities.V1.Commands.UpdateNationality;

public class UpdateNationalityCommandValidator : AbstractValidator<UpdateNationalityCommand>
{
    public UpdateNationalityCommandValidator()
    {
        RuleFor(x => x.Translation).NotNull().WithMessage("Translation Cannot be null");
        RuleFor(x => x.Id).NotNull().WithMessage("Id Cannot be null")
            .NotEmpty().WithMessage("Id Cannot be Empty");
    }
}