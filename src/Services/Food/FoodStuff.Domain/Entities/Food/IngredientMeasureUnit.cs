using Domain;
using FoodStuff.Domain.Entities.MeasureUnit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class IngredientMeasureUnit : BaseEntity
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int MeasureUnitId { get; set; }
        public MeasureUnit.MeasureUnit MeasureUnit { get; set; }
    }
}
