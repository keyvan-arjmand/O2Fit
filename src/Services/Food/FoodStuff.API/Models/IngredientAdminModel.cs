using FoodStuff.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace FoodStuff.API.Models
{

    public class IngredientAdminSelectDTO
    {
        public int Id { get; set; }
        public TranslationDto Name { get; set; }
        public string Code { get; set; }
        public string ThumbUri { get; set; }
        public List<double> NutrientValue { get; set; }
        public double Value { get; set; }
        public MeasureUnitModelDTO MessureUnit { get; set; }
    }
}
