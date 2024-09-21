using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class DietPackFoodResponseDto 
    {
        //
        public double MeasureUnitValue { get; set; }
        //
        public int CategoryChildId { get; set; }
        //
        public int FoodId { get; set; }
        //
        public string FoodName { get; set; }
        //
        public int MeasureUnitId { get; set; }
        //
        public string MeasureUnitName { get; set; }
        //
        public int Calorie { get; set; }
        //
        public string NutrientValue { get; set; }
        public double Value  { get; set; }
    }
}