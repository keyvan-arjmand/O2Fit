using Discount.Domain.Common;

namespace Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;

public class DiscountPackageNutritionist : AggregateRoot
{
    public ObjectId PackageId { get; set; }
    public int Percent { get; set; }
}