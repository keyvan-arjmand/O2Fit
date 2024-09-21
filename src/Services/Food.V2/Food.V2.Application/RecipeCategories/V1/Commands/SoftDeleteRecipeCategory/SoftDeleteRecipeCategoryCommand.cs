namespace Food.V2.Application.RecipeCategories.V1.Commands.SoftDeleteRecipeCategory;

public record SoftDeleteRecipeCategoryCommand(string Id): IRequest;