using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Recipe;

public class RecipeTipSingleDto
{
    public string FoodId { get; set; } = string.Empty;
    public TranslationDto RecipeTip { get; set; } = new();
}