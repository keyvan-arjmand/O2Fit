using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Recipe;

public class ListRecipeTipDto
{
    public string FoodId { get; set; } = string.Empty;
    public List<TranslationDto> RecipeTips { get; set; } = new();
}