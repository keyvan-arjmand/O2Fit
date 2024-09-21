using Market.Domain.Common;

namespace Market.Domain.Aggregates.AppLearnSubCategoryAggregate;

public class AppLearnSubCategory:AggregateRoot
{
    public ObjectId CategoryId { get; set; }
    public AppLearnSubCategoryTranslation CategoryName { get; set; } = new();
    public AppLearnSubCategoryTranslation Title { get; set; } = new();
}