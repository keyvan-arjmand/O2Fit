namespace Food.V2.Application.Ingredients.V1.Queries.GetIngredientAllergiesPaginatedWithSearch;

public class GetIngredientAllergiesPaginatedWithSearchQueryValidator : AbstractValidator<GetIngredientAllergiesPaginatedWithSearchQuery>
{
    public GetIngredientAllergiesPaginatedWithSearchQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("PageIndex should be greater than or equal to 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater than or equal to 1");
    }
}