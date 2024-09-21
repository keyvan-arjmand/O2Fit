using O2fitUserHistory.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
{
    public class UserTrackWorkOutModelDTO
    {
        public int Id { get; set; }
        public int? WorkOutId { get; set; }
        public Classification? Classification { get; set; }
        public int? PersonalWorkOutId { get; set; }
        public int? WorkOutAttributeValueId { get; set; }
        public double BurnedCalories { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string _id { get; set; }
    }

}
