using Payment.Domain.Aggregates.TransactionDietNutritionistPackageAggregate;
using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageAdvertiseAggregate;

public class PackageInfo : BaseEntity
{
    public TransactionDietNutritionist? TranslationTitle { get; set; }//banner
    public TransactionDietNutritionist? TranslationDescription { get; set; }//banner
    public List<ObjectId> StateId { get; set; } = new();
    public string? ImageName { get; set; }//banner
    public ObjectId PackageId { get; set; }
}