namespace Food.V2.Application.RecipeCategories.V1.Queries.GetByIdRecipeCategory;

public class GetByIdRecipeCategoryQueryValidator : AbstractValidator<GetByIdRecipeCategoryQuery>
{
    public GetByIdRecipeCategoryQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}