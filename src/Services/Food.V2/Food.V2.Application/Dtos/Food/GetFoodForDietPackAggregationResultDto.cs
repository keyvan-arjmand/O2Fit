namespace Food.V2.Application.Dtos.Food;

public class GetFoodForDietPackAggregationResultDto : IDto
{
    public string Id { get; set; } = string.Empty;
    //public string ChildCategoryId { get; set; } = string.Empty;
    //public string MeasureUnitId { get; set; } = string.Empty;
    public FoodTranslationDto Name { get; set; } = null!;
}