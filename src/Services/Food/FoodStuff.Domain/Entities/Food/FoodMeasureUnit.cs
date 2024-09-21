using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodMeasureUnit : BaseEntity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int MeasureUnitId { get; set; }
        public MeasureUnit.MeasureUnit MeasureUnit { get; set; }
    }
}
