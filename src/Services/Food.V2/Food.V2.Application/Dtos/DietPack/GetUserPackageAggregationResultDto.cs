namespace Food.V2.Application.Dtos.DietPack;

public class GetUserPackageAggregationResultDto : IDto
{
    public string Id { get; set; } = string.Empty;
    //
    public FoodMeal FoodMeal { get; set; }
    //
    public int CalorieValue { get; set; }
    //
    public int DailyCalorie { get; set; }
    //
    public List<decimal> NutrientValues { get; set; } = new();
    public DietPackTranslationDto Translation { get; set; } = null!;
    public List<GetFoodForDietPackAggregationResultDto> Foods { get; set; } = new();
    public List<DietPackFoodForAggregationDto> DietPackFoods { get; set; } = new();
    public List<MeasureUnitDto> MeasureUnits { get; set; } = new();
}