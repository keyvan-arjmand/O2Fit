namespace Food.V2.Application.Dtos.DietPack;

public class GetUserPackageDto : IDto
{
    public FoodMeal FoodMeal { get; set; }
    public int CalorieValue { get; set; }
    public List<decimal> NutrientValues { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public int DailyCalorie { get; set; }
    public List<GetUserPackageDietPackFoodDto> DietPackFoods { get; set; } = new();
}