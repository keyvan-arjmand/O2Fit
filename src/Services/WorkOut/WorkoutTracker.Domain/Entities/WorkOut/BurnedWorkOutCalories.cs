using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class BurnedWorkOutCalories : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public double Value { get; set; }
        public string _id { get; set; }
    }
}
