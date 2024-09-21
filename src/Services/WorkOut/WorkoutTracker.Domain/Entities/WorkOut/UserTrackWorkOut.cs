using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserTrackWorkOut : BaseEntity
    {
        public Nullable<int> WorkOutId { get; set; }
        [ForeignKey(nameof(WorkOutId))]
        public WorkOut WorkOut { get; set; }
        public Nullable<int> PersonalWorkOutId { get; set; }
        [ForeignKey(nameof(PersonalWorkOutId))]
        public PersonalWorkOut PersonalWorkOut { get; set; }
        public double BurnedCalories { get; set; }
        public Nullable<int> WorkOutAttributeValueId { get; set; }
        [ForeignKey(nameof(WorkOutAttributeValueId))]
        public WorkOutAttributeValue WorkOutAttributeValue { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string _id { get; set; }
    }
}
