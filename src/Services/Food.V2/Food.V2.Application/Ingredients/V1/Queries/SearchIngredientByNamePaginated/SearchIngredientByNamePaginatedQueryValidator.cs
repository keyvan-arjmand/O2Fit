namespace Food.V2.Application.Ingredients.V1.Queries.SearchIngredientByNamePaginated;

public class SearchIngredientByNamePaginatedQueryValidator : AbstractValidator<SearchIngredientByNamePaginatedQuery>
{
    public SearchIngredientByNamePaginatedQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty")
            .NotNull().WithMessage("Name can not be null");

        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage("PageIndex should greater than or equal to zero");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0).WithMessage("PageSize should greater than or equal to zero");

    }
}