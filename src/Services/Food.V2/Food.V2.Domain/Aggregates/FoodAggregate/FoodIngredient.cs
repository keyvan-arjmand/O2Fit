namespace Food.V2.Domain.Aggregates.FoodAggregate;

public class FoodIngredient : BaseEntity
{
    public FoodIngredient()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public ObjectId IngredientId { get; set; }
    public FoodTranslation IngredientTranslation { get; set; } = new();
    public List<IngredientMeasureUnit> IngredientMeasureUnits { get; set; } = new();
    public ObjectId MeasureUnitId { get; set; }
    public NotNegativeForDecimalTypes MeasureUnitValue { get; set; } = new();
    public FoodTranslation MeasureUnitTranslation { get; set; } = new();
    public NotNegativeForDecimalTypes IngredientValue { get; set; } = new();
}