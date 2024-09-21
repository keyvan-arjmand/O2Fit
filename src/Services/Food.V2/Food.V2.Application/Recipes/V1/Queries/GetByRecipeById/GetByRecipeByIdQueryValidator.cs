namespace Food.V2.Application.Recipes.V1.Queries.GetByRecipeById;

public class GetByRecipeByIdQueryValidator : AbstractValidator<GetByRecipeByIdQuery>
{
    public GetByRecipeByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("id not null or empty");
    }
}