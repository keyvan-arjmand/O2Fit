using System.Collections.Generic;

namespace FoodStuff.API.Models
{
    public class GetDietPackFoodViewModel
    {
        public int DietCategoryId { get; set; }
        public double DailyCalorie { get; set; }
       // public List<int> AlergyIds { get; set; }
       public string AllergyIds { get; set; }
    }

}
