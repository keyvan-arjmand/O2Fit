using Market.Domain.Common;

namespace Market.Domain.Aggregates.AppLearnAggregate;

public class AppLearn:AggregateRoot
{
    public AppLearnTranslation VideoUrl { get; set; } = new();
    public string ImageName { get; set; } = string.Empty;
    public AppLearnTranslation Question { get; set; } = new();
    public List<AppLearnTranslation> Answer { get; set; } = new();
    public long Useful { get; set; }
    public long UnUseful { get; set; }
    public ObjectId SubCategoryId { get; set; }
    public AppLearnTranslation SubCategoryName { get; set; } = new();
}