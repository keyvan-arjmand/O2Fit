using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using FoodStuff.Domain.Entities.Food;

namespace FoodStuff.Domain.Entities.Diet
{
   public class DietPackAlerge : BaseEntity<int>
    {
        public int IngredientId { get; set; }
        //[ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient { get; set; }

        public int DietPackId { get; set; }
        //[ForeignKey(nameof(DietPack))]
        public DietPack DietPack { get; set; }

        public int IngredientAllergyCategoryId { get; set; }
        [ForeignKey(nameof(IngredientAllergyCategoryId))]
        public IngredientAllergyCategory IngredientAllergyCategory { get; set; }
    }
}
