using FoodStuff.Domain.Common;
using System.Collections.Generic;
using System;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Service.Models
{
    public class UpdateFoodDto
    {
        public int Id { get; set; }
        public CreateTranslationDto Name { get; set; }
        public long FoodCode { get; set; }
        public int BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public List<int> FoodHabitIds { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }

        public FoodType FoodType { get; set; }
        public double Version { get; set; }
        public int BrandId { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngMeasurModel> Ingredients { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public string TagEn { get; set; }
        public string TagAr { get; set; }
        public bool IsIngredient { get; set; }
        public int PersonCount { get; set; }
        public bool IsActive { get; set; }
        public List<int> NationalityIds { get; set; }

        public FoodMeal[] FoodMealIds { get; set; }

        public List<int> FoodCategoryIds { get; set; }

        public List<int> DietCategoryIds { get; set; }
        public double GI { get; set; }
        public bool UseInDiet { get; set; }
        public List<int> SpecialDiseases { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public bool IsRecipe { get; set; }
        public int? RecipeCategoryId { get; set; }
        public UpdateRecipeDto Recipe { get; set; }
    }
}