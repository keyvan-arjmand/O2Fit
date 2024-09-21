using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
  public class UserTrackFoodModelDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Value { get; set; }
        public int FoodMeal { get; set; }
        public DateTime InsertDate { get; set; }
        public string FoodNutrientValue { get; set; }
        public int MeasureUnitId { get; set; }
        public string MeasureUnitName { get; set; }
        public Nullable<int> PersonalFoodId { get; set; }
        public Nullable<int> FoodId { get; set; }
        public string FoodName { get; set; }
        public string _id { get; set; }
    }
}
