using Track.Domain.Enums;

namespace Track.Application.Dtos;

public class UserTrackFoodDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public FoodMeal FoodMeal { get; set; }
    public DateTime InsertDate { get; set; }
    public List<decimal> FoodNutrientValue { get; set; } = new();
    public string MeasureUnitId { get; set; } = string.Empty;
    public string MeasureUnitName { get; set; } = string.Empty;
    public string? PersonalFoodId { get; set; }
    public string? FoodId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
}