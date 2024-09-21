using System;

namespace WorkoutTracker.Domain.Models
{
    public class UpdateUserTrackStepsDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int StepsCount { get; set; }
        public TimeSpan? Duration { get; set; }
        public double UserWeight { get; set; }
        public string _id { get; set; }
        public bool IsManual { get; set; }

    }
}
