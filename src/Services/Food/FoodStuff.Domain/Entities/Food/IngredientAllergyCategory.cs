using System.Collections.Generic;
using Domain;
using FoodStuff.Domain.Entities.Diet;

namespace FoodStuff.Domain.Entities.Food
{
    public class IngredientAllergyCategory : BaseEntity
    {
        public int IngredientAllergyCategoryId { get; set; }
        public ICollection<IngredientAllergyCategoryIngredientAllergy> IngredientAllergyCategoryIngredientAllergy { get; set; }
        public ICollection<DietPackAlerge> DietPackAlerges { get; set; }
    }
}