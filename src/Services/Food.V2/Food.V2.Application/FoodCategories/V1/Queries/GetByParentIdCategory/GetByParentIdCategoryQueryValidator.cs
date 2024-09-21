namespace Food.V2.Application.FoodCategories.V1.Queries.GetByParentIdCategory;

public class GetByParentIdCategoryQueryValidator:AbstractValidator<GetByParentIdCategoryQuery>
{
    public GetByParentIdCategoryQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id Cannot be null")
            .NotEmpty().WithMessage("Id Cannot be Empty");
    }
}