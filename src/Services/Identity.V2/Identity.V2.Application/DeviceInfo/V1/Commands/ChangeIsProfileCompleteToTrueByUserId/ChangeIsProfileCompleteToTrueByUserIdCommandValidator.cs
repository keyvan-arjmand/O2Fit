namespace Identity.V2.Application.DeviceInfo.V1.Commands.ChangeIsProfileCompleteToTrueByUserId;

public class ChangeIsProfileCompleteToTrueByUserIdCommandValidator : AbstractValidator<ChangeIsProfileCompleteToTrueByUserIdCommand>
{
    public ChangeIsProfileCompleteToTrueByUserIdCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
    }
}