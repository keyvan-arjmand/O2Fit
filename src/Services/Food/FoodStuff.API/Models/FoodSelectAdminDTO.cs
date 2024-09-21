using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;

namespace FoodStuff.API.Models
{
    public class FoodSelectAdminDTO
    {

        public int FoodId { get; set; }
        public string _id { get; set; }
        public TranslationDto Name { get; set; }
        public TranslationDto Recipe { get; set; }
        public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public FoodHabit FoodHabit { get; set; }
        // public string FoodHabit { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public FoodType FoodType { get; set; }
        public double Version { get; set; }
        public BrandSelectAdminDTO Brand { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<SearchResultAdminDTO> MeasureUnits { get; set; }
        public List<IngredientAdminModel> Ingredients { get; set; }
        public string Tag { get; set; }

    }

    public class UpdateFoodAdminDTO
    {
        public int Id { get; set; }
        public TranslationDto Name { get; set; }
        public TranslationDto? Recipe { get; set; }
        public long FoodCode { get; set; }
        public int BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public List<int> FoodHabitIds { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }

        public int FoodType { get; set; }
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
    }

    public class FoodAdminViewModel
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public TranslationDto Name { get; set; }
        public TranslationDto Recipe { get; set; }
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
        public BrandSelectAdminDTO Brand { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngredientAdminSelectDTO> Ingredients { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public bool IsIngredient { get; set; }

        public List<NationalityViewModel> Nationalities { get; set; }

        public FoodMeal[] FoodMealIds { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<DietCategoryViewModel> DietCategories { get; set; }
        public double GI { get; set; }
        public bool UseInDiet { get; set; }
        public List<SpecialDisease> SpecialDiseases { get; set; }

        public int DefaultMeasureUnitId { get; set; }
        public bool IsRecipe { get; set; }
    }

}
