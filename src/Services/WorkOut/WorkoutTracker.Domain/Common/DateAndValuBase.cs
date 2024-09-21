using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Common
{
    public class DateAndValuBase<TValue>
    {
        public TValue Value { get; set; }
        public DateTime date { get; set; }

    }
}
