using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class UserTrackWater : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public double Value { get; set; }
        public string _id { get; set; }
    }
}
