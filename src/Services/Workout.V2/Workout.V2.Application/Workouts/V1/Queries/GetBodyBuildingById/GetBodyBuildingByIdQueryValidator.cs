namespace Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingById;

public class GetBodyBuildingByIdQueryValidator : AbstractValidator<GetBodyBuildingByIdQuery>
{
    public GetBodyBuildingByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}