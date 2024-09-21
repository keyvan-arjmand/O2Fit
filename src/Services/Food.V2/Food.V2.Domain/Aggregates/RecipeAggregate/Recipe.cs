namespace Food.V2.Domain.Aggregates.RecipeAggregate;

public class Recipe : AggregateRoot
{
    public RecipeStatus Status { get; set; }
    public ObjectId FoodId { get; set; }
    public List<RecipeTranslation> RecipeSteps { get; set; } = new();
    public List<RecipeTranslation> RecipeTips { get; set; } = new();

}