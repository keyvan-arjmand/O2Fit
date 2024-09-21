using System;
using Amazon.Runtime.Internal;
using FoodStuff.Domain.Enum;
using MediatR;

namespace FoodStuff.Service.v2.Command.Foods
{
    public class UpdateFoodCommand : IRequest<Unit>
    {
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public double WeightBeforeBaking { get; set; }
        public double EvaporatedWater { get; set; }
        public double WeightAfterBaking { get; set; }
        public TimeSpan BakingTime { get; set; }
        public double DryIngredient { get; set; }
        public string NutrientValue { get; set; }
        public double Version { get; set; }
        public BakingType BakingType { get; set; }
        public FoodType FoodType { get; set; }
        public int? BrandId { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public bool IsActive { get; set; }
        public long FoodCode { get; set; }
        public int PersonCount { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public bool IsIngredient { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public double GI { get; set; }
        public bool UseInDiet { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public FoodMeal[] FoodMeals { get; set; }
        public bool IsRecipe { get; set; }
    }
}