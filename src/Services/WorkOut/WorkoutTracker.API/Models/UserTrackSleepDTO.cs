using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.API.Models
{
    public class UserTrackSleepDTO:BaseDto<UserTrackSleepDTO,UserTrackSleep>
    {
        public int UserId { get; set; }
        //public DateTime InsertDate { get; set; }
        public int Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BurnedCalories { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public int age { get; set; }
        public Gender gender { get; set; }
        public string _id { get; set; }
    }
}
