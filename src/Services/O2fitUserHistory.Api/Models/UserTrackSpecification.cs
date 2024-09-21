using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
{
    public class UserTrackSpecification 
    {
        public int Id { get; set; }
        public Nullable<double> WeightSize { get; set; }
        public Nullable<double> NeckSize { get; set; }
        public Nullable<double> ShoulderSize { get; set; }
        public Nullable<double> ArmSize { get; set; }
        public Nullable<double> WristSize { get; set; }
        public Nullable<double> BustSize { get; set; }
        public Nullable<double> WaistSize { get; set; }
        public Nullable<double> HighHipSize { get; set; }
        public Nullable<double> HipSize { get; set; }
        public Nullable<double> ThighSize { get; set; }
        public DateTime InsertDate { get; set; }
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string _id { get; set; }
    }
}
