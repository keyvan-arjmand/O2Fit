using System;
using System.Collections.Generic;
using System.Text;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Domain.Models
{
    public class ActivityTime
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
