namespace Food.V2.Domain.Aggregates.RecipeAggregate;

public class RecipeTip : BaseAuditableEntity
{
    public RecipeTranslation Translation { get; set; } = null!;
}