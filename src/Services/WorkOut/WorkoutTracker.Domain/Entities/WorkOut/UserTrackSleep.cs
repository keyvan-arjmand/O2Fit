using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserTrackSleep : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int Rate { get; set; }
        public TimeSpan Duration { get; set; }
        public double BurnedCalories { get; set; }
        public string _id { get; set; }
    }
}
