using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Food;

public class FilterFoodDto
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Name { get; set; } = new ();
    public List<decimal> NutrientValue { get; set; } = new ();
}