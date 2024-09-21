namespace Identity.V2.Application.Users.V1.Commands.ChangeUserStatusByUserId;

public class ChangeUserStatusByUserIdCommandValidator : AbstractValidator<ChangeUserStatusByUserIdCommand>
{
    public ChangeUserStatusByUserIdCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");

        RuleFor(x => x.Status).IsInEnum();
    }
}