using Ordering.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Models
{
    public class CreatePackageDTO
    {
        [Required]
        public TranslationDTO TranslationName { get; set; }
        [Required]
        public TranslationDTO TranslationDescription { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double? DiscountPercent { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public PackageType PackageType { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public bool IsPromote { get; set; }
        [Required]
        public int NutritionistId { get; set; }
    }
}
