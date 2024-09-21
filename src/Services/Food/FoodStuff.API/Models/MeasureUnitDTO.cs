using FoodStuff.Domain.Entities.MeasureUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class MeasureUnitDTO:BaseDto<MeasureUnitDTO,MeasureUnit,int>
    {
        public Nullable<int> NameId { get; set; }
        public TranslationDto Name { get; set; }
        public double Value { get; set; }
        public int meassureUnitCategory{ get; set; }
    }
}
