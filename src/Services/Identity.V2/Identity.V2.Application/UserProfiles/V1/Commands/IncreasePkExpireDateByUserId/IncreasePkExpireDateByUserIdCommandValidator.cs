namespace Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkExpireDateByUserId;

public class IncreasePkExpireDateByUserIdCommandValidator : AbstractValidator<IncreasePkExpireDateByUserIdCommand>
{
    public IncreasePkExpireDateByUserIdCommandValidator()
    {
        RuleFor(x=>x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
    }
}