namespace Food.V2.Application.RecipeCategories.V1.Queries.IsRecipeCategoryExists;

public record IsRecipeCategoryExistsQuery(string Id) : IRequest<bool>;