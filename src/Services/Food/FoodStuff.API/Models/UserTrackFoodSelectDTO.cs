using Data.Repositories;
using FoodStuff.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStuff.API.Models
{
    public class UserTrackFoodSelectDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<int> FoodId { get; set; }
        public Nullable<int> PersonalFoodId { get; set; }
        public string FoodName { get; set; }
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }
        public string MeasureUnitName { get; set; }
        public int FoodMeal { get; set; }
        public string FoodMealName { get; set; }
        public List<double> FoodNutrientValue { get; set; }
        public string _id { get; set; }
    }
}
