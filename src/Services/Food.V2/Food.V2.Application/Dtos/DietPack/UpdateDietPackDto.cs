namespace Food.V2.Application.Dtos.DietPack;

public class UpdateDietPackDto
{
    public string Id { get; set; } = string.Empty;
    public FoodMeal FoodMeal { get; set; }
    public int DailyCalorie { get; set; }
    public int CalorieValue { get; set; }

    public List<string> DietCategoryIds { get; set; } = new();

    public List<decimal> NutrientValues { get; set; } = new();

    public List<string> NationalityIds { get; set; } = new();

    public List<CreateDietPackFoodDto> DietPackFoods { get; set; } = new();
    public List<SpecialDisease> SpecialDiseases { get; set; } = new();
    public bool IsActive { get; set; }
    public string ParentCategoryId { get; set; } = string.Empty;
}