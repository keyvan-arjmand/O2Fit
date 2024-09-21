namespace Food.V2.Domain.Aggregates.DietPackAggregate;

public class DietPackFood : BaseEntity
{
    public DietPackFood()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public ObjectId FoodId { get; set; }
    public ObjectId MeasureUnitId { get; set; }
    public ObjectId ChildCategoryId { get; set; }
    public string MeasureUnitName { get; set; } = string.Empty;
    public decimal MeasureUnitValue { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public NotNegativeForDecimalTypes Value { get; set; } = null!;
    public NonNegativeForIntegerTypes Calorie { get; set; } = null!;
    public List<NotNegativeForDecimalTypes> NutrientValues { get; set; } = new();

}