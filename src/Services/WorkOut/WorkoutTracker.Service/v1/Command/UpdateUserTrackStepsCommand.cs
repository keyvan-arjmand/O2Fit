using MediatR;
using System;

namespace WorkoutTracker.Service.v1.Command
{
    public class UpdateUserTrackStepsCommand : IRequest<Unit>
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
