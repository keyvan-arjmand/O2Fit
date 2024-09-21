using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserTrackFoodByCalorieDTO
    {
        public int UserId { get; set; }
        public List<double> FoodNutrientValue { get; set; }
        public int FoodMeal { get; set; }
        public DateTime InsertDate { get; set; }
        public string _id { get; set; }
    }
}
