using Common.Enums.Foods;

namespace Track.Application.Dtos;

public class TrackMealDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public decimal CalorieValue { get; set; }
    public FoodMealEvent Meal { get; set; }
    public string FoodMealName { get; set; } = string.Empty;
    public List<FoodMealDto> Foods { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}