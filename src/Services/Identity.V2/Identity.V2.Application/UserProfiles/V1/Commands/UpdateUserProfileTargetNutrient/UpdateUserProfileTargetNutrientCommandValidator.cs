namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTargetNutrient;

public class UpdateUserProfileTargetNutrientCommandValidator : AbstractValidator<UpdateUserProfileTargetNutrientCommand>
{
    public UpdateUserProfileTargetNutrientCommandValidator()
    {
        RuleFor(x=>x.UserId).NotNull().WithMessage("UserId can not be null")
            .NotEmpty().WithMessage("UserId can not be empty");
    }
}