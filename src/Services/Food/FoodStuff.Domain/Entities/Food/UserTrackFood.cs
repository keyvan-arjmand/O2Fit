using Domain;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class UserTrackFood : BaseEntity
    {
        public int UserId { get; set; }
        public float Value { get; set; }
        public int FoodMeal { get; set; }
        public DateTime InsertDate { get; set; }
        public string FoodNutrientValue { get; set; }
        public int MeasureUnitId { get; set; }
        [ForeignKey(nameof(MeasureUnitId))]
        public MeasureUnit.MeasureUnit MeasureUnit { get; set; }

        public Nullable<int> PersonalFoodId { get; set; }
        public PersonalFood PersonalFood { get; set; }

        public Nullable<int> FoodId { get; set; }
        public Food Food { get; set; }
        public string _id { get; set; }
    }
}
