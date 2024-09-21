using Common.Enums.Foods;

namespace Track.Application.Dtos;

public class FoodMealDto
{
    public string Id { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal  MeasureUnitValue { get; set; }
    public FoodMealEvent FoodMeal { get; set; }
    public List<decimal> NutrientValue { get; set; } = new();
    public string MeasureUnitId { get; set; }= string.Empty;
    public string MeasureUnitName { get; set; } = string.Empty;
    public string? PersonalFoodId { get; set; }
    public string? FoodId { get; set; }
    public string  Name { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
    
}