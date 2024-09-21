using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetFoodAlergy
{
   public class GetFoodAlergyQueryResult
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }
        public string _id { get; set; }
        public int Id { get; set; }
    }
}
