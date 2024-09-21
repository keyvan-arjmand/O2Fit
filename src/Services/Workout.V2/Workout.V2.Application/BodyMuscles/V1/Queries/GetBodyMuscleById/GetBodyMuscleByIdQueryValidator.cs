namespace Workout.V2.Application.BodyMuscles.V1.Queries.GetBodyMuscleById;

public class GetBodyMuscleByIdQueryValidator : AbstractValidator<GetBodyMuscleByIdQuery>
{
    public GetBodyMuscleByIdQueryValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}