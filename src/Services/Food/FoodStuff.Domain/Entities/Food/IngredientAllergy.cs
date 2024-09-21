using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class IngredientAllergy : BaseEntity<int>
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public ICollection<IngredientAllergyCategoryIngredientAllergy> IngredientAllergyCategoryIngredientAllergy { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
