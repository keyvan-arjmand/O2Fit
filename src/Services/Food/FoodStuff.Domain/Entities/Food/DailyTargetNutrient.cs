using Domain;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class DailyTargetNutrient : BaseEntity
    {
        public Nutrient Nutrient { get; set; }
        public Gender Gender { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public string NutrientValue { get; set; }
    }
}
