using System;
using Domain;
using WebFramework.Api;

namespace User.API.Models
{
    public class UserTrackSpecificationDto : BaseDto<UserTrackSpecificationDto, UserTrackSpecification, int>
    {
        public Nullable<double> WeightSize { get; set; }
        public Nullable<double> BustSize { get; set; }
        public Nullable<double> ArmSize { get; set; }
        public Nullable<double> WaistSize { get; set; }
        public Nullable<double> HighHipSize { get; set; }
        public Nullable<double> HipSize { get; set; }
        public Nullable<double> ThighSize { get; set; }
        public Nullable<double> NeckSize { get; set; }
        public Nullable<double> ShoulderSize { get; set; }
        public Nullable<double> WristSize { get; set; }
        public DateTime InsertDate { get; set; }
        public string _id { get; set; }
    }
}