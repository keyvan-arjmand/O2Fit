namespace Workout.V2.Application.Workouts.V1.Queries.GetCardioPaginated;

public class GetCardioPaginatedQueryValidator : AbstractValidator<GetCardioPaginatedQuery>
{
    public GetCardioPaginatedQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page should be greater than zero");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page should be greater than zero");
    }
}