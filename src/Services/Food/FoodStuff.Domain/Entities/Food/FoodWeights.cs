using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodWeights
    {
        public double BeforeBaking { get; set; }
        public double AfterBaking { get; set; }
        public double EvaporatedWater { get; set; }
        public double DryIngredient { get; set; }
        public double Water { get; set; }
    }
}
