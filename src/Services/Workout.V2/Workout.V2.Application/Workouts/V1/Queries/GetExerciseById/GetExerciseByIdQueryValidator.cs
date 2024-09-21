namespace Workout.V2.Application.Workouts.V1.Queries.GetExerciseById;

public class GetExerciseByIdQueryValidator : AbstractValidator<GetExerciseByIdQuery>
{
    public GetExerciseByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}