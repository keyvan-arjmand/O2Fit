namespace Workout.V2.Application.BodyMuscles.V1.Commands.DeleteBodyMuscle;

public class DeleteBodyMuscleCommandValidator : AbstractValidator<DeleteBodyMuscleCommand>
{
    public DeleteBodyMuscleCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}