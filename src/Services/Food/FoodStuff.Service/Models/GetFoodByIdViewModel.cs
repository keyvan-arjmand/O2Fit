using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class GetFoodByIdViewModel
    {
        public int FoodId { get; set; }
        public string _id { get; set; }
        public TranslationViewModel Name { get; set; }
        public TranslationViewModel Recipe { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public List<FoodHabit> FoodHabitIds { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public int PersonCount { get; set; }
        public FoodType FoodType { get; set; }

        public TranslationViewModel Brand { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<MeasureUnitViewModel> MeasureUnits { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
        public FoodMeal[] FoodMeals { get; set; }
        public double GI { get; set; }
        public int DefaultMeasureUnitId { get; set; }
    }
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public TranslationViewModel Name { get; set; }
        public double Value { get; set; }

        public int MeasureUnitId { get; set; }
        public List<MeasureUnitViewModel> MeasureUnitList { get; set; }

    }
    public class MeasureUnitViewModel
    {
        public int Id { get; set; }
        public TranslationViewModel MeasureUnitName { get; set; }
        public double Value { get; set; }
    }
}
