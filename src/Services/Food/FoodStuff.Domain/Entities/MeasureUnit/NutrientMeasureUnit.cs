using Domain;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.MeasureUnit
{
    public class NutrientMeasureUnit : BaseEntity
    {
        public Nutrient Nutrient { get; set; }
        public int MeasureUnitId { get; set; }
        [ForeignKey(nameof(MeasureUnitId))]
        public MeasureUnit MeasureUnit { get; set; }
    }
}
