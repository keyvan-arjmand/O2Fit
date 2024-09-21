namespace Food.V2.Application.Dtos.DietPack;

public class DietPackFoodDto : IDto
{
    public string FoodId { get; set; } = string.Empty;
    public int Calorie { get; set; }
    public string MeasureUnitId { get; set; } = string.Empty;
    public string ChildCategoryId { get; set; } = string.Empty;
    public List<decimal> NutrientValues { get; set; } = new();
    public string MeasureUnitName { get; set; } = string.Empty;
    public decimal MeasureUnitValue { get; set; } 
    public string FoodName { get; set; } = string.Empty;
}