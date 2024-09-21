namespace Food.V2.Application.RecipeCategories.V1.Commands.UpdateRecipeCategory;

public record UpdateRecipeCategoryCommand(string Id ,string ImageUri, CreateUpdateRecipeCategoryTranslationDto TranslationDto) : IRequest;