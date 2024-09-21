using System;
using System.Collections.Generic;
using FoodStuff.Domain.Enum;
using MediatR;

namespace FoodStuff.Service.v2.Command.Foods
{
    public class CreateFoodCommand : IRequest<int>
    {
        public int NameId { get; set; }
        public int? RecipeId { get; set; }

        public List<double> IngredientCalculate { get; set; }
        public double SumWeight { get; set; }


        public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public double WeightBeforeBaking { get; set; }
        public double WeightAfterBaking { get; set; }
        public double EvaporatedWater { get; set; }
        public double DryIngredient { get; set; }
        public FoodType FoodType { get; set; }
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
        public int? RecipeCategoryId { get; set; }
    }
}