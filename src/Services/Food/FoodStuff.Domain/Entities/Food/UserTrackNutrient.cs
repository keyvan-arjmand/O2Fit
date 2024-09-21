using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class UserTrackNutrient : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Value { get; set; }
        public string _id { get; set; }
    }
}
