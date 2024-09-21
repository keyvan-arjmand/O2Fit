using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class IngredientAllergyCategoryIngredientAllergy : BaseEntity
    {
        public int IngredientAllergyCategoryId { get; set; }
        [ForeignKey(nameof(IngredientAllergyCategoryId))]
        public IngredientAllergyCategory IngredientAllergyCategory { get; set; }
        public int IngredientAllergyId { get; set; }
        [ForeignKey(nameof(IngredientAllergyId))]
        public IngredientAllergy IngredientAllergy { get; set; }
    }
}