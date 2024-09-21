using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Common;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.API.Models
{
    public class UserTrackSleepSelectDTO
    {
        public int UserId { get; set; }
        public double AvrageRate { get; set; }
        public double AvrageBurnedCalories { get; set; }
        public TimeSpan AvrageSleepDuration { get; set; }
        public List<SleepValueAndDateModel> SleepDetails { get; set; }

    }
    public class SleepValueAndDateModel: DateAndValuBase<double>
    {
        public int Id { get; set; }
        public Nullable<DateTime> startdate  { get; set; }
        public Nullable<DateTime> enddate  { get; set; }
        public TimeSpan Duration { get; set; }
        public double DailyAvragRate { get; set; }
        public string _Id { get; set; }
    }
}
