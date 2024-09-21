using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Recipe;

public class RecipeStepDto
{
    public string FoodId { get; set; } = string.Empty;
    public TranslationDto RecipeStep { get; set; } = new();
}