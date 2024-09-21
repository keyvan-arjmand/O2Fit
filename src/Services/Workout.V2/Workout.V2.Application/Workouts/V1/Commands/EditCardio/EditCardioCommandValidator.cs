namespace Workout.V2.Application.Workouts.V1.Commands.EditCardio;

public class EditCardioCommandValidator : AbstractValidator<EditCardioCommand>
{
    public EditCardioCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
        RuleFor(x => x.Name.Arabic).NotEmpty().WithMessage("Arabic can not be empty").NotNull().WithMessage("Arabic can not be null");
        RuleFor(x => x.Name.English).NotEmpty().WithMessage("English can not be empty").NotNull().WithMessage("English can not be null");
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull().WithMessage("Persian can not be null");
        RuleFor(x => x.Icon).NotEmpty().WithMessage("Icon can not be empty").NotNull().WithMessage("Icon can not be null");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Image can not be empty").NotNull().WithMessage("Image can not be null");
        RuleFor(x => x.Video).NotEmpty().WithMessage("Video can not be empty").NotNull().WithMessage("Video can not be null");
        RuleFor(x => x.BurnedCalories).GreaterThanOrEqualTo(0).WithMessage("BurnedCalories should greater than zero");
        RuleFor(x => x.Level).IsInEnum();
        RuleFor(x => x.CardioCategory).IsInEnum();
        RuleFor(x => x.Gender).IsInEnum();
    }
}