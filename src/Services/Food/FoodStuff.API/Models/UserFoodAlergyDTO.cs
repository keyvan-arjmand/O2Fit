using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserFoodAlergyDTO:BaseDto<UserFoodAlergyDTO,UserFoodAlergy,int>
    {
        public int UserId { get; set; }
        public int IngredientId { get; set; }
        public string _id { get; set; }
    }
}
