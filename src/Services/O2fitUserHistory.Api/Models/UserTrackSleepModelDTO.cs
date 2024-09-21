using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
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
    }
}
