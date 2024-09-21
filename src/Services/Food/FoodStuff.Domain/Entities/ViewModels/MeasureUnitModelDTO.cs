using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class MeasureUnitModelDTO
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
        public MeasureUnitCategory MeasureUnitCategory { get; set; }
    }
}
