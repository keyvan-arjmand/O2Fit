using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class FoodViewModel
    {
        public int FoodId { get; set; }
        public string _id { get; set; }
        public TranslationViewModel Name { get; set; }
        public TranslationViewModel Recipe { get; set; }
        //public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public List<FoodHabit> FoodHabitIds { get; set; }
        // public string FoodHabit { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public int PersonCount { get; set; }
        public FoodType FoodType { get; set; }
        public double Version { get; set; }
        public bool IsUpdate { get; set; }
        public TranslationViewModel Brand { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngredientAdminModel> Ingredients { get; set; }
        public FoodMeal[] FoodMeals { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<NationalityViewModel> Nationalities { get; set; }
        public List<DietCategoryViewModel> DietCategories { get; set; }
        public double GI { get; set; }
        public int DefaultMeasureUnitId { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public Translation.Translation NameTranslation { get; set; }

        public int? ParentId { get; set; }

        public float? Percent { get; set; }
    }

    public class NationalityViewModel
    {
        public int Id { get; set; }
        public Translation.Translation NameTranslation { get; set; }

        public int? ParentId { get; set; }
    }

    public class DietCategoryViewModel
    {
        public int Id { get; set; }
        public Translation.Translation NameTranslation { get; set; }
        public Translation.Translation DescriptionTranslation { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
    }
}
