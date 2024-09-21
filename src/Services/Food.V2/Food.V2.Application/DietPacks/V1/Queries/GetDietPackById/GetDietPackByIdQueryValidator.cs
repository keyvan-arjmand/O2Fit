namespace Food.V2.Application.DietPacks.V1.Queries.GetDietPackById;

public class GetDietPackByIdQueryValidator : AbstractValidator<GetDietPackByIdQuery>
{
    public GetDietPackByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}