

using System.Collections.Generic;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class DietPackFoodDto
    {
        public int FoodId { get; set; }
        public int Calorie { get; set; }
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }

        public int CategoryChildId { get; set; }
        public List<double> NutrientValue { get; set; }
    }
}
