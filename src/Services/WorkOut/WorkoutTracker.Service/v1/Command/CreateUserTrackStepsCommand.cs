using MediatR;
using System;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Service.v1.Command
{
    public class CreateUserTrackStepsCommand : IRequest<UserTrackSteps>
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int StepsCount { get; set; }
        public TimeSpan? Duration { get; set; }
        public double UserWeight { get; set; }
        public string _id { get; set; }
        public bool IsManual { get; set; }
    }
}
