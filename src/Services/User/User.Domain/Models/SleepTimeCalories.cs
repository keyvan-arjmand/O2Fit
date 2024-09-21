using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Models
{
    public sealed class SleepTimeCalories
    {
        public double TotalCalories { get; set; }
        public double DurationAverage { get; set; }
        public double RateAverage { get; set; }
    }
}
