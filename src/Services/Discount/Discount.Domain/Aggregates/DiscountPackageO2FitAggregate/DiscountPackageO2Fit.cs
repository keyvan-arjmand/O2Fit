using Common.Enums.TypeEnums;
using Discount.Domain.Common;

namespace Discount.Domain.Aggregates.DiscountPackageO2FitAggregate
{
    public class DiscountPackageO2Fit : AggregateRoot
    {
        public ObjectId PackageId { get; set; } 
        public int Percent { get; set; }
        public PackageType PackageType { get; set; } 
    }
}
