using Ordering.Domain.Entities.Package;
using Ordering.Domain.Entities.Translation;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.API.Models
{
    public class PackageDto : BaseDto<PackageDto, Package>
    {
        public Translation TranslationName { get; set; }
        public Translation TranslationDescription { get; set; }
        public double Price { get; set; }
        public double? DiscountPercent { get; set; }
        public int Duration { get; set; }
        public Currency Currency { get; set; }
        public PackageType PackageType { get; set; }
        public bool IsActive { get; set; }
        public int Sort { get; set; }
        public int NutritionistId { get; set; }
    }
}
