using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodIngredient : BaseEntity
    {
        public int FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        public Food Food { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient { get; set; }
        public int MeasureUnitId { get; set; }
        [ForeignKey(nameof(MeasureUnitId))]
        public MeasureUnit.MeasureUnit MeasureUnit { get; set; }
        public double IngredientValue { get; set; }
    }
}
