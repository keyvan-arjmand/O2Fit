using Common.Enums.Foods;
using Track.Application.Dtos;
using Track.Domain.Enums;


namespace Track.Application.TrackFoods.V1.Commands.InsertUserTrackFoodByCalorie;

public class InsertUserTrackFoodByCalorieCommand:IRequest<UserTrackFoodDto>
{
    public string UserId { get; set; } = string.Empty;
    public List<decimal> FoodNutrientValue { get; set; } = new();
    public FoodMeal FoodMeal { get; set; }
    public DateTime InsertDate { get; set; }
    public string AppId { get; set; }= string.Empty;
    public string  FoodName { get; set; } = string.Empty;
    public string MeasureUnitName { get; set; } = string.Empty;

}