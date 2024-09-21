using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserTrackSteps : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int StepsCount { get; set; }
        public double BurnedCalories { get; set; }
        public Nullable<TimeSpan> Duration { get; set; }
        public string _id { get; set; }
        public bool IsManual { get; set; }
    }
}
