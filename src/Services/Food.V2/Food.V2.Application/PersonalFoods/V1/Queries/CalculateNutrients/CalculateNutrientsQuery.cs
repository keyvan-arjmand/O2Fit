namespace Food.V2.Application.PersonalFoods.V1.Queries.CalculateNutrients;

public class CalculateNutrientsQuery:IRequest<List<decimal>>
{
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public List<IngredientMeasureUnitDto> Ingredients { get; set; } = new();

}