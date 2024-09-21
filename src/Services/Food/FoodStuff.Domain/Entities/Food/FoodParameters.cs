using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
   public class FoodParameters
    {
        public List<double> foodNutrients { get; set; }
        public double foodWeight { get; set; }
        public double BakingRatio { get; set; }
        public double BakingTimeInMinute { get; set; }
    }
}
