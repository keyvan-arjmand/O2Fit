using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class Category : BaseEntity
    {
       
        public int NameId { get; set; }

        [ForeignKey(nameof(NameId))]
        public Translation.Translation NameTranslation { get; set; }

        public int? ParentId { get; set; }

        public float? Percent { get; set; }

        public ICollection<FoodCategory> FoodCategories { get; set; }
    }
}