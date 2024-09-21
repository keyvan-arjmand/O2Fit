namespace Food.V2.Application.RecipeCategories.V1.Commands.CreateRecipeCategory;

public record CreateRecipeCategoryCommand(string ImageUri, CreateUpdateRecipeCategoryTranslationDto TranslationDto): IRequest<string>;