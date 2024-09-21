using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserTrackNutrientDTO:BaseDto<UserTrackNutrientDTO, UserTrackNutrient>
    {
      public int UserId { get; set; }
      public DateTime InsertDate { get; set; }
      public string Value { get; set; }
    
    }
}
