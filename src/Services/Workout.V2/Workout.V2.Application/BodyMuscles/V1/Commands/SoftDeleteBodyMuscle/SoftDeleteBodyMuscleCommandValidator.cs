namespace Workout.V2.Application.BodyMuscles.V1.Commands.SoftDeleteBodyMuscle;

public class SoftDeleteBodyMuscleCommandValidator : AbstractValidator<SoftDeleteBodyMuscleCommand>
{
    public SoftDeleteBodyMuscleCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}