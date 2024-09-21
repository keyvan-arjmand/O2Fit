namespace Food.V2.Application.Foods.V1.Queries.GetFoodByName;

public class GetFoodByNameQueryValidator:AbstractValidator<GetFoodByNameQuery>
{
    public GetFoodByNameQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).NotEmpty().NotNull().WithMessage("page cannot null or empty");
        RuleFor(x => x.PageSize).GreaterThan(0).NotEmpty().NotNull().WithMessage("PageSize cannot null or empty");
        RuleFor(x => x.Lang).NotEmpty().WithMessage("Lang cannot empty");
        RuleFor(x => x.FoodName).NotEmpty().WithMessage("FoodName cannot null");
    }
}