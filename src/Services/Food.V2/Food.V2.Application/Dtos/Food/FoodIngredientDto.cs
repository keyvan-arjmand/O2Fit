using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Food;

public class FoodIngredientDto
{
    public string Id { get; set; } = string.Empty;
    public string IngredientId { get; set; } = string.Empty;
    public TranslationDto Name { get; set; } = new();
    public string MeasureUnitId { get; set; } = string.Empty;
    public decimal MeasureUnitValue { get; set; } = new();
    public TranslationDto MeasureUnitName { get; set; }= new();
    public decimal Value { get; set; }= new();
    public List<MeasureUnitsDto> MeasureUnits { get; set; }= new(); //=>Ingredient
    
}