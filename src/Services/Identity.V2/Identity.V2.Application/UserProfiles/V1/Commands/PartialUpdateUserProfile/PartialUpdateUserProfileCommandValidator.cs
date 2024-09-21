namespace Identity.V2.Application.UserProfiles.V1.Commands.PartialUpdateUserProfile;

public class PartialUpdateUserProfileCommandValidator : AbstractValidator<PartialUpdateUserProfileCommand>
{
    public PartialUpdateUserProfileCommandValidator()
    {
        RuleFor(x=>x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
        
        //RuleFor(x=>x.FullName).NotEmpty().WithMessage("FullName can not be empty")
        //    .NotNull().WithMessage("FullName can not be null");
        
        RuleFor(x=>x.HeightSize).NotEmpty().WithMessage("HeightSize can not be empty")
            .NotNull().WithMessage("HeightSize can not be null").GreaterThanOrEqualTo(60).WithMessage("Minimum value of height size is 60 cm")
            .LessThanOrEqualTo(230).WithMessage("Maximum value of height size is 230 cm");

        RuleFor(x => x.Gender).IsInEnum().WithMessage("Gender is enum");
        
        RuleFor(x=>x.FoodHabit).IsInEnum().WithMessage("FoodHabit is enum");

        RuleFor(x => x.DailyActivityRate).IsInEnum().WithMessage("DailyActivityRate is enum");
    }
}