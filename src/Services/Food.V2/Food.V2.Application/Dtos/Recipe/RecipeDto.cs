using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Enums;

namespace Food.V2.Application.Dtos.Recipe;

[BsonIgnoreExtraElements]
public class RecipeDto
{
    public string FoodId { get; set; } = string.Empty;
    public RecipeStatus Status { get; set; }
    public List<TranslationDto> RecipeSteps { get; set; } = new();
    public List<TranslationDto> RecipeTips { get; set; } = new();
}