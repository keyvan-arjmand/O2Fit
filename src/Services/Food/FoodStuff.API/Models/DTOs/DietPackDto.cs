using FoodStuff.Domain.Entities.ViewModels;
using System.Collections.Generic;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Enum;
using WebFramework.Api;

namespace FoodStuff.API.Models.DTOs
{
    public class DietPackDto: BaseDto<DietPackDto, DietPack, int>
    {
        //
        public FoodMeal FoodMeal { get; set; }
        //
        public double CalorieValue { get; set; }
        public List<int> DietCategoryIds { get; set; }
        //
        public bool IsActive { get; set; }
        //
        public List<double> NutrientValue { get; set; }
        public List<int> NationalityIds { get; set; }
        public string Name { get; set; }
        public List<DietPackFoodResponseDto> DietPackFoods { get; set; }
        //
        public int CategoryId { get; set; }
        //
        public double DailyCalorie { get; set; }
    }
}