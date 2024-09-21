using Market.Domain.Common;

namespace Market.Domain.Aggregates.FAQCategoryAggregate;

public class FaqCategory : AggregateRoot
{
 
    public FaqCategoryTranslation Title { get; set; } = new();
}