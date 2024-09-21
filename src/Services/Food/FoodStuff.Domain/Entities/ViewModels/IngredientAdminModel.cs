using System;
using System.Collections.Generic;
using System.Text;
using FoodStuff.Domain.Entities.Translation;

namespace FoodStuff.Domain.Entities.ViewModels
{
   public class IngredientAdminModel
    {
        public int Id { get; set; }
        public Translation.Translation Name { get; set; }
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }
        // public TranslationDto MeasureUnitName { get; set; }
        public List<int> MeasureUnitList { get; set; }
    }
}
