namespace Workout.V2.Application.Workouts.V1.Queries.GetCardioById;

public class GetCardioByIdQueryValidator : AbstractValidator<GetCardioByIdQuery>
{
    public GetCardioByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}