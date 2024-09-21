namespace Food.V2.Domain.Aggregates.IngredientAggregate;

public class Ingredient : AggregateRoot
{
    public IngredientTranslation Translation { get; set; } = new();
    public string ThumbName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string? TagEn { get; set; } 
    public string? TagAr { get; set; } 
    public ObjectId DefaultMeasureUnitId { get; set; }
    public bool IsAllergy { get; set; }
    public List<ObjectId> MeasureUnitIds { get; set; } = new();
    public List<NotNegativeForDecimalTypes> NutrientValue { get; set; } = new();
}