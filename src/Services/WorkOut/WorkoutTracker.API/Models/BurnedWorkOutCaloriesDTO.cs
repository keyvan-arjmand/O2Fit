using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.API.Models
{
    public class BurnedWorkOutCaloriesDTO:BaseDto<BurnedWorkOutCaloriesDTO, BurnedWorkOutCalories>
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public double Value { get; set; }
    }
}
