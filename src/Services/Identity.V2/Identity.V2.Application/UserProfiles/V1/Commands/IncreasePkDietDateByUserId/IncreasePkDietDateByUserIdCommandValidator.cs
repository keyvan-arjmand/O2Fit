namespace Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkDietDateByUserId;

public class IncreasePkDietDateByUserIdCommandValidator : AbstractValidator<IncreasePkDietDateByUserIdCommand>
{
    public IncreasePkDietDateByUserIdCommandValidator()
    {
        RuleFor(x=>x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
    }
}