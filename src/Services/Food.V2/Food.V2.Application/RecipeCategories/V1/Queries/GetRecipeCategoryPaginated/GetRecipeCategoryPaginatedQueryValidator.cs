namespace Food.V2.Application.RecipeCategories.V1.Queries.GetRecipeCategoryPaginated;

public class GetRecipeCategoryPaginatedQueryValidator : AbstractValidator<GetRecipeCategoryPaginatedQuery>
{
    public GetRecipeCategoryPaginatedQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("PageIndex should greater than or equal to 1")
            .NotEmpty().WithMessage("PageIndex can not be empty");
        
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize should greater than or equal to 1")
            .NotEmpty().WithMessage("PageSize can not be empty");
    }
}