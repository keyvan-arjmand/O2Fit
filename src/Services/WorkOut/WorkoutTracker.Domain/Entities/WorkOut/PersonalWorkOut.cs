using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class PersonalWorkOut : BaseEntity
    {
        public int UserId { get; set; }
        public double Calorie { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string _id { get; set; }
    }
}
