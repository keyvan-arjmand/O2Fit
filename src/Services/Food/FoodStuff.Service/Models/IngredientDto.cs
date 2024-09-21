using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public TranslationResultDto Name { get; set; }
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }
        // public TranslationDto MeasureUnitName { get; set; }
        public List<int> MeasureUnitList { get; set; }
    }
}