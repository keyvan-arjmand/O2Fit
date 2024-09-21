namespace Food.V2.Application.Dtos.DietPack;

public class DietPackFoodForAggregationDto: IDto
{
    public string Id { get; set; } = string.Empty;
    public string ChildCategoryId { get; set; } = string.Empty;
    public decimal Value { get; set; }
}