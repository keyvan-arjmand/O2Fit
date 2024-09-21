using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Service.v1.Command.CreatUserTrackWorkout
{
   public class CreatUserTrackWorkoutCommand:IRequest<CreatUserTrackWorkoutResult>
    {
        public UserTrackWorkOut userTrackWorkOut { get; set; }
        public double duration { get; set; }
        public double Weight { get; set; }


    }
}
