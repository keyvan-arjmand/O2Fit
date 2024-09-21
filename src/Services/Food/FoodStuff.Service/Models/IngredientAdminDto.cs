using FoodStuff.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class IngredientAdminDto
    {
        public int Id { get; set; }
        public TranslationResultDto Name { get; set; }
        public string Code { get; set; }
        public string ThumbUri { get; set; }
        public List<double> NutrientValue { get; set; }
        //public string NutrientValue { get; set; }

        public double Value { get; set; }
        public MeasureUnitAdminDto MeasureUnit { get; set; }
    }
}