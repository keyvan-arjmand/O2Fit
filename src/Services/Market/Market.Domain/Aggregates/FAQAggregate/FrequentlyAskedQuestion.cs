using Market.Domain.Common;

namespace Market.Domain.Aggregates.FAQAggregate;

public class FrequentlyAskedQuestion : AggregateRoot
{
    public FaqTranslation Question { get; set; } = new();
    public FaqTranslation Answer { get; set; } = new();
    public long Useful { get; set; }
    public long UnUseful { get; set; }
    public ObjectId CategoryId { get; set; }
    public FaqTranslation CategoryName { get; set; } = new();
}