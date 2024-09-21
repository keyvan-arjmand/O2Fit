namespace Food.V2.Domain.Aggregates.PersonalFoodAggregate;

public class PersonalFood : AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public NotNegativeForDecimalTypes WeightBeforeBaking { get; set; } = new();
    public NotNegativeForDecimalTypes WeightAfterBaking { get; set; } = new();
    public NotNegativeForDecimalTypes EvaporatedWater { get; set; } = new();
    public decimal DryIngredient { get; set; }
    public NotNegativeForDecimalTypes Water { get; set; } = new();
    public ObjectId? ParentFoodId { get; set; } //Food
    public bool IsCopyable { get; set; }
    public List<NotNegativeForDecimalTypes> NutrientValue { get; set; } = new();
    public List<PersonalFoodIngredient> PersonalFoodIngredients { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
}