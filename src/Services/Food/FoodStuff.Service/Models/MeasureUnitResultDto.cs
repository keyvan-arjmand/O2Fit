using System;

namespace FoodStuff.Service.Models
{
    public class MeasureUnitResultDto
    {
        public int? NameId { get; set; }
        public TranslationResultDto Name { get; set; }
        public double Value { get; set; }
        public int meassureUnitCategory { get; set; }
    }
}