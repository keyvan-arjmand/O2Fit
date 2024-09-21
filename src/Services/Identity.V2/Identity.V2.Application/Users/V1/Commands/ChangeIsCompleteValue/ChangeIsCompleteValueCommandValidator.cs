namespace Identity.V2.Application.Users.V1.Commands.ChangeIsCompleteValue;

public class ChangeIsCompleteValueCommandValidator : AbstractValidator<ChangeIsCompleteValueCommand>
{
    public ChangeIsCompleteValueCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
    }
}