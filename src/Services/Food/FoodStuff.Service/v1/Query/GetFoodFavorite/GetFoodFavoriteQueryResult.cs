using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetFoodFavorite
{
   public class GetFoodFavoriteQueryResult
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; } //= Food Id
        public string _id { get; set; }
    }
}
