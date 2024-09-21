using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Models
{
    public sealed class SleepCalorieModel
    {
        public double Calories { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
