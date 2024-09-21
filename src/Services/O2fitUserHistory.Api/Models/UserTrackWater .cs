using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
{
    public class UserTrackWater 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public double Value { get; set; }
        public string _id { get; set; }
    }
}
