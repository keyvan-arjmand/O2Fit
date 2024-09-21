namespace Food.V2.Domain.Aggregates.FoodAggregate;

public class IngredientMeasureUnit : BaseEntity
{
    public NotNegativeForDecimalTypes Value { get; set; } = new();
    public bool IsActive { get; set; }
    public FoodTranslation Translation { get; set; } = null!;
}