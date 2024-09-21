using System.Collections.Generic;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStuff.Domain.Entities.Food
{
    public class RecipeCategore : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation NameTranslation { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; } = false;

        //public ICollection<Food> Foods { get; set; }
    }

}
