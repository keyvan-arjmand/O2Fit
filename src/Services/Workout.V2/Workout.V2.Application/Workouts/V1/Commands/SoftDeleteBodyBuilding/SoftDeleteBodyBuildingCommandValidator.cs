namespace Workout.V2.Application.Workouts.V1.Commands.SoftDeleteBodyBuilding;

public class SoftDeleteBodyBuildingCommandValidator : AbstractValidator<SoftDeleteBodyBuildingCommand>
{
    public SoftDeleteBodyBuildingCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}