using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Common
{
   public class MeasureValuBase<TValue>
    {
        public TValue Value { get; set; }
        public int MeasureUnitId { get; set; }
        public string MeasureUnitName { get; set; }
        public int MeasureUnitNameId { get; set; }
    }
    public class DateAndValuBase<TValue>
    {
        public TValue Value { get; set; }
        public DateTime date { get; set; }

    }

}
