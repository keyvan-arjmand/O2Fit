using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System.Collections.Generic;
using System;

namespace FoodStuff.Service.Models
{
    public class FoodWithDetailsDto
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public TranslationResultDto Name { get; set; }
        public TranslationResultDto OldRecipe { get; set; }
        public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public int BakingTimeInMinutes { get; set; }

        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
         public List<FoodHabit> FoodHabitIds { get; set; }
        public string FoodHabit { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public int PersonCount { get; set; }
        public FoodType FoodType { get; set; }
        public double Version { get; set; }
        public bool IsUpdate { get; set; }
        public TranslationResultDto Brand { get; set; }
        public string NutrientValue { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngredientDto> Ingredients { get; set; } //= new List<IngredientDto>();
        public FoodMeal[] FoodMeals { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<NationalityDto> Nationalities { get; set; }
        public List<DietCategoryResultDto> DietCategories { get; set; }
        public double GI { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public int? RecipeCategoryId { get; set; }
        public RecipeDto Recipe { get; set; }
        public int Calories { get; set; }



    }
}