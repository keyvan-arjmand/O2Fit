using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class UserFoodFavoriteSelectDTO
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; } //= Food Id
        public string _id { get; set; }
    }
}
