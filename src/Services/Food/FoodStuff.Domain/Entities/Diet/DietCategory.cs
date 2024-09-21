using Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FoodStuff.Domain.Entities.Food;

namespace FoodStuff.Domain.Entities.Diet
{
   public class DietCategory:BaseEntity<int>
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation NameTranslation { get; set; }
        public int? DescriptionId { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        public Translation.Translation DescriptionTranslation { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public bool IsPromote { get; set; } = false;
        public string BannerImage { get; set; }
        public ICollection<DietPackDietCategory> DietPackDietCategories { get; set; }
        public ICollection<FoodDietCategory> FoodDietCategories { get; set; }
    }
}
