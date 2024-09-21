using System;
using System.Collections.Generic;
using System.Text;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Service.v1.Command.CreatUserTrackWorkout
{
   public class CreatUserTrackWorkoutResult
    {
        public int Id { get; set; }
        public int? WorkOutId { get; set; }
        public Classification? Classification { get; set; }
        public int? PersonalWorkOutId { get; set; }
        public int? WorkOutAttributeValueId { get; set; }
        public double BurnedCalories { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string _id { get; set; }
    }
}
