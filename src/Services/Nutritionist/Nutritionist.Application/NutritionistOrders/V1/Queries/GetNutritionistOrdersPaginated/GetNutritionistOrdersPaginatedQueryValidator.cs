namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetNutritionistOrdersPaginated;

public class GetNutritionistOrdersPaginatedQueryValidator : AbstractValidator<GetNutritionistOrdersPaginatedQuery>
{
    public GetNutritionistOrdersPaginatedQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Language).IsInEnum();
    }
}