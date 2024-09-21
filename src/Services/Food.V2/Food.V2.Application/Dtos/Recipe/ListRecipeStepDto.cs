using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Recipe;

public class ListRecipeStepDto
{
    public string FoodId { get; set; } = string.Empty;
    public List<TranslationDto> RecipeSteps { get; set; } = new();
}