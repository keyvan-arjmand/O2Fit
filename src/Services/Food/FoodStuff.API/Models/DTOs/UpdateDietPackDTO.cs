using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodStuff.API.Models.DTOs
{
    public class UpdateDietPackDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public FoodMeal FoodMeal { get; set; }
        [Required]
        public double DailyCalorie { get; set; }
        [Required]
        public double CalorieValue { get; set; }
        [Required]
        public List<int> DietCategoryIds { get; set; }
        [Required]
        public List<double> NutrientValue { get; set; }
        [Required]
        public List<int> NationalityIds { get; set; }
        [Required]
        public List<FoodStuff.Domain.Entities.ViewModels.DietPackFoodDto> DietPackFoods { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
