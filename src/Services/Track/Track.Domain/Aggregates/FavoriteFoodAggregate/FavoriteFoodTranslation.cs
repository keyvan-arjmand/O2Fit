using Track.Domain.Common;

namespace Track.Domain.Aggregates.FavoriteFoodAggregate;

public class FavoriteFoodTranslation : BaseEntity
{
    public FavoriteFoodTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}