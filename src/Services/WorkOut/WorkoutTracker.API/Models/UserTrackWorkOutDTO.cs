using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserTrackWorkOutDTO : BaseDto<UserTrackWorkOutDTO,UserTrackWorkOut>
    {
        public int? WorkOutId { get; set; }
        public int? PersonalWorkOutId { get; set; }
        public int? WorkOutAttributeValueId { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public TimeSpan Duration { get; set; }
        public double Weight { get; set; }
        public string _id { get; set; }
    }
}
