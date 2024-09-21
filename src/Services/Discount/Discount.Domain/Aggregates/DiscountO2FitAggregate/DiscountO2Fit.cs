using Common.Enums.TypeEnums;
using Discount.Domain.Common;
using Discount.Domain.ValueObjects;

namespace Discount.Domain.Aggregates.DiscountO2FitAggregate;

public class DiscountO2Fit: AggregateRoot
{
    public DiscountCode Code { get; set; } = default!;
    public DiscountO2FitTranslation Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDateTime { get; set; }
    public ObjectId? UserId { get; set; } //for user 
    public List<int>? CountryId { get; set; } //access to discount
    public int Percent { get; set; } 
    public int UsableCount { get; set; }
    public bool IsActive { get; set; }
    public DiscountType DiscountType { get; set; }
    public PackageType PackageType { get; set; }
}