using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
{
    public class UserTrackSteps 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int StepsCount { get; set; }
        public double BurnedCalories { get; set; }
        public Nullable<TimeSpan> Duration { get; set; }
        public string _id { get; set; }
    }
}
