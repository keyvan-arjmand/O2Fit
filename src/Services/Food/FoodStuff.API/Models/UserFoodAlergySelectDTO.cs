using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserFoodAlergySelectDTO:BaseDto<UserFoodAlergyDTO,UserFoodAlergy,int>
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }
        public string _id { get; set; }
    }
}
