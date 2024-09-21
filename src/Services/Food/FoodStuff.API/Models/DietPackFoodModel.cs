using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.API.Models
{
    public class DietPackFoodModel
    {
        public int Id { get; set; }
        public FoodMeal FoodMeal { get; set; }
        public double CalorieValue { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public bool IsActive { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> NationalityIds { get; set; }
        public DietPackFoodViewModel DietPackFoods { get; set; }

        public int CategoryId { get; set; }
        public double DailyCalorie { get; set; }


    }
}
