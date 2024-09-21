using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Enums;

namespace Food.V2.Application.Dtos.Recipe;

public class FullRecipeDto
{
    public RecipeStatus Status { get; set; }
    public string FoodId { get; set; } = string.Empty;
    public TranslationDto FoodName { get; set; } = new();
    public string RecipeCategoryId { get; set; } = string.Empty;

    public List<TranslationDto> RecipeSteps { get; set; } = new();
    public List<TranslationDto> RecipeTips { get; set; } = new();
    
}