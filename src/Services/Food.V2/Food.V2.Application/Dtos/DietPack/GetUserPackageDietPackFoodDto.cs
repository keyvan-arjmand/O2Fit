namespace Food.V2.Application.Dtos.DietPack;

public class GetUserPackageDietPackFoodDto : IDto
{
    public decimal MeasureUnitValue { get; set; }

  //  public string CategoryChildId { get; set; } = string.Empty;

    public string FoodId { get; set; } = string.Empty;

    public string FoodName { get; set; } = string.Empty;

    public string MeasureUnitId { get; set; } = string.Empty;

    public string MeasureUnitName { get; set; } = string.Empty;
    public int Calorie { get; set; }

    public List<decimal> NutrientValues { get; set; } = new();
    public decimal Value { get; set; }
}