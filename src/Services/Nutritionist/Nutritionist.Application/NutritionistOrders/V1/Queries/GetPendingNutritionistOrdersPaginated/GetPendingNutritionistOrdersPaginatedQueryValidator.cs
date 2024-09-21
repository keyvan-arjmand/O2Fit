namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetPendingNutritionistOrdersPaginated;

public class GetPendingNutritionistOrdersPaginatedQueryValidator : AbstractValidator<GetPendingNutritionistOrdersPaginatedQuery>
{
    public GetPendingNutritionistOrdersPaginatedQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Language).IsInEnum();
    }
}