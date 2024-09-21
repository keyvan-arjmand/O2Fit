using Track.Domain.Common;

namespace Track.Domain.Aggregates.FavoriteRecipeAggregate;

public class FavoriteRecipe : AggregateRoot
{
    public int UserId { get; set; }
    public ObjectId FoodId { get; set; }
}