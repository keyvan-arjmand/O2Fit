using Domain;
using FoodStuff.Domain.Entities.MeasureUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class PersonalFoodIngredient : BaseEntity
    {
        public int PersonalFoodId { get; set; }
        [ForeignKey(nameof(PersonalFoodId))]
        public PersonalFood PersonalFood { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient { get; set; }
        public int MeasureUnitId { get; set; }
        [ForeignKey(nameof(MeasureUnitId))]
        public MeasureUnit.MeasureUnit MeasureUnit { get; set; }
        public double IngredientValue { get; set; }
        public string _id { get; set; }
    }
}
