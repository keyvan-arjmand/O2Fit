namespace Food.V2.Application.Dtos.DietPack;

public class DietPackDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public FoodMeal FoodMeal { get; set; }
    public int CalorieValue { get; set; }
    public List<string> DietCategoryIds { get; set; } = new();
    public bool IsActive { get; set; }
    public List<decimal> NutrientValues { get; set; } = new();
    public List<string> NationalityIds { get; set; } = new();
    public string ParentCategoryId { get; set; } = string.Empty;
    public int DailyCalorie { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<DietPackFoodDto> DietPackFoods { get; set; } = new();
}