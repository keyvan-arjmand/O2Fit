using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserTrackFoodDTO:BaseDto<UserTrackFoodDTO, UserTrackFood>
    {
        public int UserId { get; set; }
        public int? PersonalFoodId { get; set; }
        public int? FoodId { get; set; }
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }
        public int FoodMeal { get; set; }
        public DateTime InsertDate { get; set; }
        public List<double> FoodNutrientValue { get; set; }
        public string _id { get; set; }
    }
}
