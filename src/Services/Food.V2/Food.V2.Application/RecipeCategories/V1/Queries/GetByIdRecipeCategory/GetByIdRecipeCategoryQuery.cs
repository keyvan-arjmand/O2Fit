namespace Food.V2.Application.RecipeCategories.V1.Queries.GetByIdRecipeCategory;

public record GetByIdRecipeCategoryQuery(string Id) : IRequest<RecipeCategoryDto>;