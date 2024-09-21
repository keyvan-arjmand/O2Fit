using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Food;

public class SearchFoodNameDto
{
    public string FoodId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<decimal> NutrientValue { get; set; } = new();
    public TranslationDto BrandName { get; set; } = new();
    public FoodType FoodType { get; set; }
    public List<MeasureUnitDto> MeasureUnits { get; set; } = null!;
    public string FoodCode { get; set; } = string.Empty;
}