using Ordering.Domain.Enum;

namespace Ordering.Domain.Models
{
    public class UpdatePackageDTO
    {
        public int Id { get; set; }
        public TranslationDTO TranslationName { get; set; }

        public TranslationDTO TranslationDescription { get; set; }

        public double Price { get; set; }
        public double? DiscountPercent { get; set; }
        public int Duration { get; set; }
        public Currency Currency { get; set; }
        public PackageType PackageType { get; set; }
        public bool IsActive { get; set; }
        public int Sort { get; set; }

        //for promote 
        public bool IsPromote { get; set; }

        public int NutritionistId { get; set; }
    }
}
