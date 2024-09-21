using FoodStuff.Domain.Enum;
using System.Collections.Generic;
using System;

namespace FoodStuff.Service.Models
{
    public class FoodWithDetailForAdminDto
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public TranslationResultDto Name { get; set; }
        public TranslationResultDto OldRecipe { get; set; }
        public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public List<FoodHabit> FoodHabitIds { get; set; }

        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public int PersonCount { get; set; }
        public FoodType FoodType { get; set; }
        public double Version { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsActive { get; set; }
        public BrandDto Brand { get; set; }
        //public List<double> NutrientValue { get; set; }
        public string NutrientValue { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngredientAdminDto> Ingredients { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public bool IsIngredient { get; set; }

        public List<NationalityAdminDto> Nationalities { get; set; }

        public FoodMeal[] FoodMealIds { get; set; }

        public List<CategoryAdminDto> Categories { get; set; }

        public List<DietCategoryAdminDto> DietCategories { get; set; }
        public double GI { get; set; }
        public bool UseInDiet { get; set; }
        public List<SpecialDisease> SpecialDiseases { get; set; }

        public int DefaultMeasureUnitId { get; set; }
        public bool IsRecipe { get; set; }
        public int? RecipeCategoryId { get; set; }

        public RecipeDto Recipe { get; set; }
    }
}