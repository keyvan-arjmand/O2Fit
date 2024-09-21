namespace Food.V2.Domain.Aggregates.RecipeAggregate;

public class RecipeStep : BaseAuditableEntity
{
    public RecipeTranslation RecipeTranslation { get; set; } = null!;
}