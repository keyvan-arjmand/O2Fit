namespace Food.V2.Domain.Aggregates.RecipeCategoryAggregate;

public class RecipeCategory: AggregateRoot
{
    public string ImageName { get; set; } = string.Empty;
    public RecipeCategoryTranslation Translation { get; set; } = null!;
}