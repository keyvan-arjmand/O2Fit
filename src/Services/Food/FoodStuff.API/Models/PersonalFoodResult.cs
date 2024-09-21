using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStuff.API.Models
{
    public class PersonalFoodResult
    {
        public string FoodName { get; set; }
        public int PersonalFoodId { get; set; }
        public int? FoodId { get { return null; }}
        public string _id { get; set; }
    }
}
