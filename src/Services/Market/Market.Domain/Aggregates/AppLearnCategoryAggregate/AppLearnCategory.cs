using Market.Domain.Common;

namespace Market.Domain.Aggregates.AppLearnCategoryAggregate;

public class AppLearnCategory : AggregateRoot
{
    public AppLearnCategoryTranslation Title { get; set; } = new();
}