namespace Food.V2.Application.Recipes.V1.Queries.GetAllPagingRecipe;

public class GetAllPagingRecipeQueryValidator : AbstractValidator<GetAllPagingRecipeQuery>
{
    public GetAllPagingRecipeQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("page must Greater Than0");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must Greater Than0");
    }
}