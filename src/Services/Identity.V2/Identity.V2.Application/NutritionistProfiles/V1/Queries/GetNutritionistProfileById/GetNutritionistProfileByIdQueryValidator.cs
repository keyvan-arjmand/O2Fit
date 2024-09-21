namespace Identity.V2.Application.NutritionistProfiles.V1.Queries.GetNutritionistProfileById;

public class GetNutritionistProfileByIdQueryValidator : AbstractValidator<GetNutritionistProfileByIdQuery>
{
    public GetNutritionistProfileByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}