using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.API.Models
{
    public class UserTrackSleepModelDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; }
        public DateTime EndDate { get; set; }//InsertDate
        public string Duration { get; set; }
        public double BurnedCalories { get; set; }
        public string _id { get; set; }
        public Nullable<DateTime> InsertDate { get; set; }
    }
}
