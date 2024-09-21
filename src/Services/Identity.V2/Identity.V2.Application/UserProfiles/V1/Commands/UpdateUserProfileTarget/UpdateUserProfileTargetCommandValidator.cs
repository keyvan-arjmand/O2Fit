namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTarget;

public class UpdateUserProfileTargetCommandValidator : AbstractValidator<UpdateUserProfileTargetCommand>
{
    public UpdateUserProfileTargetCommandValidator()
    {
       // RuleFor(x => x.DailyActivityRate).IsInEnum().WithMessage("DailyActivityRate is enum");
        //RuleFor(x => x.TargetStep).NotNull().WithMessage("TargetStep can not be null")
        //    .NotEmpty().WithMessage("TargetStep can not be empty");
        //RuleFor(x=>x.TargetWeight).NotNull().WithMessage("TargetWeight can not be null")
        //    .NotEmpty().WithMessage("TargetWeight can not be empty");
        //RuleFor(x=>x.WeightChangeRate).NotNull().WithMessage("WeightChangeRate can not be null")
        //    .NotEmpty().WithMessage("WeightChangeRate can not be empty");
        RuleFor(x=>x.UserId).NotNull().WithMessage("UserId can not be null")
            .NotEmpty().WithMessage("UserId can not be empty");
    }
}