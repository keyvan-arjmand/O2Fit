namespace Workout.V2.Application.BodyMuscles.V1.Commands.EditBodyMuscle;

public class EditBodyMuscleCommandValidator : AbstractValidator<EditBodyMuscleCommand>
{
    public EditBodyMuscleCommandValidator()
    {
        RuleFor(x => x.Arabic).NotEmpty().WithMessage("Arabic can not be empty").NotNull().WithMessage("Arabic can not be null");
        RuleFor(x => x.English).NotEmpty().WithMessage("English can not be empty").NotNull().WithMessage("English can not be null");
        RuleFor(x => x.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull().WithMessage("Persian can not be null");
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}