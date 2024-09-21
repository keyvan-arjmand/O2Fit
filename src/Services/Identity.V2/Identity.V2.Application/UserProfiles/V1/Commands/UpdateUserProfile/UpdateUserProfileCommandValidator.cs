namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandValidator: AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x=>x.TargetThighSize).NotEmpty().WithMessage("TargetThighSize can not be empty")
            .NotNull().WithMessage("TargetThighSize can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetThighSize is 0");
        
        RuleFor(x=>x.TargetStep).NotEmpty().WithMessage("TargetStep can not be empty")
            .NotNull().WithMessage("TargetStep can not be null");

        RuleFor(x=>x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");

        //RuleFor(x=>x.FullName).NotEmpty().WithMessage("FullName can not be empty")
        //    .NotNull().WithMessage("FullName can not be null");

        RuleFor(x=>x.HeightSize).NotEmpty().WithMessage("HeightSize can not be empty")
            .NotNull().WithMessage("HeightSize can not be null").GreaterThanOrEqualTo(60).WithMessage("Minimum value of height size is 60 cm")
            .LessThanOrEqualTo(230).WithMessage("Maximum value of height size is 230 cm");

       // RuleFor(x=>x.ImageUri).NotEmpty().WithMessage("ImageUri can not be empty")
       //     .NotNull().WithMessage("ImageUri can not be null");

        RuleFor(x=>x.TargetWeight).NotEmpty().WithMessage("TargetWeight can not be empty")
            .NotNull().WithMessage("TargetWeight can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetWeight is 0");

        RuleFor(x=>x.TargetNeckSize).NotEmpty().WithMessage("TargetNeckSize can not be empty")
            .NotNull().WithMessage("TargetNeckSize can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetNeckSize is 0");

        RuleFor(x=>x.WeightChangeRate).NotEmpty().WithMessage("WeightChangeRate can not be empty")
            .NotNull().WithMessage("WeightChangeRate can not be null")
            .LessThanOrEqualTo(999).WithMessage("Maximum value of WeightChangeRate is 999");

        RuleFor(x => x.FoodHabit).IsInEnum().WithMessage("FoodHabit is enum");
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Gender is enum");
        RuleFor(x => x.DailyActivityRate).IsInEnum().WithMessage("DailyActivityRate is enum");
    }
}