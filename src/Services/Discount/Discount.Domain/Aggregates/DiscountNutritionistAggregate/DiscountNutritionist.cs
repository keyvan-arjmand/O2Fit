using Common.Enums.TypeEnums;
using Discount.Domain.Common;
using Discount.Domain.ValueObjects;

namespace Discount.Domain.Aggregates.DiscountNutritionistAggregate;

public class DiscountNutritionist:AggregateRoot
{
    public DiscountCode Code { get; set; } = default!;
    public DiscountNutritionistTranslation Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDateTime { get; set; }
    public ObjectId? UserId { get; set; } //for user 
    public List<int> CountryId { get; set; } = new(); //access to discount
    public double Amount { get; set; }
    public int UsableCount { get; set; }
    public bool IsActive { get; set; }
    public DiscountType DiscountType { get; set; }
    public PackageType PackageType { get; set; }
}