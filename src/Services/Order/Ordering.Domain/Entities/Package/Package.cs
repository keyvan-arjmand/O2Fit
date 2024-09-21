using Domain;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordering.Domain.Entities.Package
{
    public class Package : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation TranslationName { get; set; }

        public int DescriptionId { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        public Translation.Translation TranslationDescription { get; set; }

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
