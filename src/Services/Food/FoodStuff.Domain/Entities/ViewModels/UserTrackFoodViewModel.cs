using System;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class UserTrackFoodViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Value { get; set; }
        public int FoodMeal { get; set; }
        public DateTime InsertDate { get; set; }

        public string FoodNutrientValue { get; set; }

        public int MeasureUnitId { get; set; }
        public string? MeasureUnitName { get; set; }

        public int? PersonalFoodId { get; set; }
        public string? PersonalFoodName { get; set; }

        public string? FoodName { get; set; }

        public int? FoodId { get; set; }
        public string _id { get; set; }

    }
}
