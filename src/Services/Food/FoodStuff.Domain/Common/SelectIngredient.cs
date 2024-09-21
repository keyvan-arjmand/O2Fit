using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Common
{
    public class SelectIngredient
    {
        public List<double> IngredientCalculate { get; set; }
        public double SumWeight { get; set; }
    }
}
