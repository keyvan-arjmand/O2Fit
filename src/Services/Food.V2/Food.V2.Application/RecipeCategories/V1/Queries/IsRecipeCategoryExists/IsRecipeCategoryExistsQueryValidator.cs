namespace Food.V2.Application.RecipeCategories.V1.Queries.IsRecipeCategoryExists;

public class IsRecipeCategoryExistsQueryValidator : AbstractValidator<IsRecipeCategoryExistsQuery>
{
    public IsRecipeCategoryExistsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}