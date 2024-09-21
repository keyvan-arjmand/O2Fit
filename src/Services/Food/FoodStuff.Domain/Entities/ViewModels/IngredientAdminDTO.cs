using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
   public class IngredientAdminDTO
    {
        public int Id { get; set; }
        public Translation.Translation Name { get; set; }
        public int Code{ get; set; }
        public string ThumbUri { get; set; }
        public string NutrientValue { get; set; }
        public List<MeasureUnitModelDTO> MessureUnits { get; set; }
    }
}
