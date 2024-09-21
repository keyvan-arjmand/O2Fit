using Domain;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Diet
{
   public class DietAlergy: BaseEntity<int>
    {
        public int MainAlergyId { get; set; }
        public Ingredient MainAlergy { get; set; }
        public int IngredientAlergyId { get; set; }

    }
}
