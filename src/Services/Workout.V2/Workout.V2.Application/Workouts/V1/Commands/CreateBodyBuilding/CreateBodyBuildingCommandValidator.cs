namespace Workout.V2.Application.Workouts.V1.Commands.CreateBodyBuilding;

public class CreateBodyBuildingCommandValidator : AbstractValidator<CreateBodyBuildingCommand>
{
    public CreateBodyBuildingCommandValidator()
    {
        RuleFor(x => x.Name.Arabic).NotEmpty().WithMessage("Arabic can not be empty").NotNull().WithMessage("Arabic can not be null");
        RuleFor(x => x.Name.English).NotEmpty().WithMessage("English can not be empty").NotNull().WithMessage("English can not be null");
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull().WithMessage("Persian can not be null");
        RuleFor(x => x.Icon).NotEmpty().WithMessage("Icon can not be empty").NotNull().WithMessage("Icon can not be null");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Image can not be empty").NotNull().WithMessage("Image can not be null");
        RuleFor(x => x.Video).NotEmpty().WithMessage("Video can not be empty").NotNull().WithMessage("Video can not be null");
        RuleFor(x => x.BurnedCalories).GreaterThanOrEqualTo(0).WithMessage("BurnedCalories should greater than zero");
        RuleFor(x => x.Level).IsInEnum();
        RuleFor(x => x.Gender).IsInEnum();
        RuleFor(x => x.TargetMuscle).IsInEnum();
        RuleForEach(x=>x.BodyMuscleIds).NotEmpty().WithMessage("BodyMuscleIds can not be empty").NotNull().WithMessage("BodyMuscleIds can not be null");
    }
}