using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserTrackWaterDTO:BaseDto<UserTrackWaterDTO,UserTrackWater,int>
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public double Value { get; set; }
        public string _id { get; set; }
    }
}
