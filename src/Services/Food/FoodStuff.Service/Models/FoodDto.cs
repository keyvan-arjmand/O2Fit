using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FoodStuff.Service.Models
{
    public class FoodDto
    {
        public int NameId { get; set; }
        public TranslationResultDto TranslationName { get; set; }

        public int? RecipeId { get; set; }
        public TranslationResultDto TranslationRecipe { get; set; }
        public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public double WeightBeforBaking { get; set; }
        public double WeightAfterBaking { get; set; }
        public double EvaporatedWater { get; set; }
        public double DryIngredient { get; set; }
        public FoodType FoodType { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpdate { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public string NutrientValue { get; set; }
        public int? BrandId { get; set; }
        public double Version { get; set; }
        public bool IsIngredient { get; set; }
        public int PersonCount { get; set; }
        public FoodMeal[] FoodMeals { get; set; }

        public double GI { get; set; }

        public bool UseInDiet { get; set; }

        public int DefaultMeasureUnitId { get; set; }

        public bool IsRecipe { get; set; }
        public int IsRecipeCategoreId { get; set; }
        public int? RecipeCategoryId { get; set; }
    }
}