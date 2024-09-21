namespace Food.V2.Application.Dtos.DietPack;

public class CreateDietPackFoodDto : IDto
{
    public string FoodId { get; set; } = string.Empty;
    public string MeasureUnitId { get; set; } = string.Empty;
    public string ChildCategoryId { get; set; } = string.Empty;
    public string MeasureUnitName { get; set; } = string.Empty;
    public decimal MeasureUnitValue { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public int Calorie { get; set; }
    public List<decimal> NutrientValues { get; set; } = new();
}