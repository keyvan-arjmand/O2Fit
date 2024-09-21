using Domain;
using WorkoutTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserTrackWorkOutSelectDTO : BaseDto<UserTrackWorkOutSelectDTO, UserTrackWorkOut>
    {
        public SelectOption<int> workOut { get; set; }
        public double BurnedCalories { get; set; }
        public SelectOption<int> WorkOutAttribute { get; set; }
        public SelectOption<int> WorkOutAttributeValue { get; set; }
        public int UserId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime InsertDate { get; set; }
        public string _id { get; set; }
    }
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
