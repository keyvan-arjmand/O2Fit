using Track.Application.Dtos;
using Track.Domain.Enums;

namespace Track.Application.TrackFoods.V1.Commands.InsertUserTrackFood;

public class InsertUserTrackFoodCommand : IRequest<UserTrackFoodDto>
{
    public string UserId { get; set; } = string.Empty;
    public string? PersonalFoodId { get; set; }
    public string? FoodId { get; set; }
    public decimal Value { get; set; }
    public string MeasureUnitId { get; set; } = string.Empty;
    public FoodMeal FoodMeal { get; set; }
    public DateTime InsertDate { get; set; }
    public List<decimal> FoodNutrientValue { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
    public string FoodName { get; set; } = string.Empty;
    public string MeasureUnitName { get; set; } = string.Empty;
}