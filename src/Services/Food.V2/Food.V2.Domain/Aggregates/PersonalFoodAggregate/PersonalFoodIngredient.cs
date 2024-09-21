using Food.V2.Domain.Aggregates.FoodAggregate;

namespace Food.V2.Domain.Aggregates.PersonalFoodAggregate;

public class PersonalFoodIngredient : BaseEntity
{
    public PersonalFoodIngredient()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public ObjectId IngredientId { get; set; }
    public PersonalFoodTranslation IngredientTranslation { get; set; } = new();
    public List<IngredientMeasureUnit> IngredientMeasureUnits { get; set; } = new();
    public ObjectId MeasureUnitId { get; set; }
    public NotNegativeForDecimalTypes MeasureUnitValue { get; set; } = new();
    public PersonalFoodTranslation MeasureUnitTranslation { get; set; } = new();
    public NotNegativeForDecimalTypes IngredientValue { get; set; } = new();
    
}