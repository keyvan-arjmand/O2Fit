using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserFoodFavoriteDTO:BaseDto<UserFoodFavoriteDTO,UserFoodFavorite,int>
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public string _id { get; set; }
    }
}
