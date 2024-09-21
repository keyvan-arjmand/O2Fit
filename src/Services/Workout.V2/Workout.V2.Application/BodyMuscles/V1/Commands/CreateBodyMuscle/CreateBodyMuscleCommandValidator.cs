namespace Workout.V2.Application.BodyMuscles.V1.Commands.CreateBodyMuscle;

public class CreateBodyMuscleCommandValidator : AbstractValidator<CreateBodyMuscleCommand>
{
    public CreateBodyMuscleCommandValidator()
    {
        RuleFor(x => x.Translation.Arabic).NotEmpty().WithMessage("Arabic can not be empty").NotNull().WithMessage("Arabic can not be null");
        RuleFor(x => x.Translation.English).NotEmpty().WithMessage("English can not be empty").NotNull().WithMessage("English can not be null");
        RuleFor(x => x.Translation.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull().WithMessage("Persian can not be null");

    }
}