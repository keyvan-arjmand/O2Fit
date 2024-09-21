using System.Collections.Generic;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Service.Models
{
    public class DietPackDto
    {
        //
        public FoodMeal FoodMeal { get; set; }
        //
        public double CaloriValue { get; set; }
        public string NutrientValue { get; set; }
       //
        public string Name { get; set; }
        public List<DietPackFoodResponseDto> DietPackFoods { get; set; }
        //
        public double DailyCalorie { get; set; }
    }
}