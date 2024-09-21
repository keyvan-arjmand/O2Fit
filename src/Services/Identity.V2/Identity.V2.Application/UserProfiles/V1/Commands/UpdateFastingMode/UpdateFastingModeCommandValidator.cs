namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateFastingMode;

public class UpdateFastingModeCommandValidator : AbstractValidator<UpdateFastingModeCommand>
{
    public UpdateFastingModeCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
    }
}