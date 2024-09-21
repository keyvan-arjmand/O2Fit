namespace Food.V2.Application.Recipes.V1.Queries.GetFullRecipeById;

public class GetFullRecipeByIdQueryValidator:AbstractValidator<GetFullRecipeByIdQuery>
{
    public GetFullRecipeByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id Cannot Null or empty");
    }
}