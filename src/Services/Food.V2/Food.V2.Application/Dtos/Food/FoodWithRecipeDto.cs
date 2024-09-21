using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Food;

public class FoodWithRecipeDto
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Name { get; set; } = new();
    public long? FoodCode { get; set; }
    public string RecipeId { get; set; } = string.Empty;
    public RecipeDto? Recipe { get; set; } 
}