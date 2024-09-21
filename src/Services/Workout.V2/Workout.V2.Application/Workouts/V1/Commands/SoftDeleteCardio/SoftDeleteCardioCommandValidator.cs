namespace Workout.V2.Application.Workouts.V1.Commands.SoftDeleteCardio;

public class SoftDeleteCardioCommandValidator : AbstractValidator<SoftDeleteCardioCommand>
{
    public SoftDeleteCardioCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}