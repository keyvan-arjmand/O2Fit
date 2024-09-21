using System.Collections.Generic;
using FoodStuff.Domain.Entities.Diet;
using WebFramework.Api;

namespace FoodStuff.API.Models.DTOs
{
    public class DietPackFoodResponseDto : BaseDto<DietPackFoodResponseDto, DietPackFood,int>
    {
        public double MeasureUnitValue { get; set; }
        //
        public int CategoryChildId { get; set; }
        //
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        //
        public int MeasureUnitId { get; set; }
        public string MeasureUnitName { get; set; }
        //
        public int Calorie { get; set; }
        //
        public List<double> NutrientValue { get; set; }
    }
}