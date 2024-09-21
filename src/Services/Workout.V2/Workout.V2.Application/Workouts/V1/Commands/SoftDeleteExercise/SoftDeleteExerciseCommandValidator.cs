namespace Workout.V2.Application.Workouts.V1.Commands.SoftDeleteExercise;

public class SoftDeleteExerciseCommandValidator : AbstractValidator<SoftDeleteExerciseCommand>
{
    public SoftDeleteExerciseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");

    }
}