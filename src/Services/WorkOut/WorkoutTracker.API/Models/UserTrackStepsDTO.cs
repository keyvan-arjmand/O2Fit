using System;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserTrackStepsDTO
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int StepsCount { get; set; }
        public Nullable<TimeSpan> Duration { get; set; }
        public double UserWeight { get; set; }
        public string _id { get; set; }
        public bool IsManual { get; set; }

    }



}
