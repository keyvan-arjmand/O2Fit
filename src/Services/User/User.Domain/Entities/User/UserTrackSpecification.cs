using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class UserTrackSpecification : BaseEntity
    {
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
        [ForeignKey(nameof(UserProfileId))]
        public UserProfile UserProfiles { get; set; }
        public string _id { get; set; }
    }

}
