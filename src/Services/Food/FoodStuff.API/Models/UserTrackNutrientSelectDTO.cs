using Data.Repositories;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserTrackNutrientSelectDTO
    {
      public DateTime InsertDate { get; set; }
      public List<UserTrackNutrientModel> userTrackNutrient { get; set; }
    }
    public class UserTrackNutrientModel:MeasureValuBase<double>
    {
        public SelectOption<int> nutrient { get; set; }
        
    }
    public class ReportNutrientModel
    {
        public SelectOption<int> Nutrient { get; set; }
        public SelectOption<int> MeasureUnit { get; set; }
        public List<DateAndValuBase<double>> ValuInDates { get; set; }
    } 
    
    public class NutrientReport
    {
        public SelectOption<int> Nutrient { get; set; }
        public SelectOption<int> MeasureUnit { get; set; }
        public List<DateAndValuBase<double>> ValuInDates { get; set; }
    }



}
