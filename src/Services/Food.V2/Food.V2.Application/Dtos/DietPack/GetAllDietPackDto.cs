namespace Food.V2.Application.Dtos.DietPack;

public class GetAllDietPackDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public int FoodMeal { get; set; }
    public int CalorieValue { get; set; }
    public bool IsActive { get; set; }
    public int DailyCalorie { get; set; }
    public List<string> DietCategoryIds { get; set; } = new();
    public List<string> NationalityIds { get; set; } = new();
}