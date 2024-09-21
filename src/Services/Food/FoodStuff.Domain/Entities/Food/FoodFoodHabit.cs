using Domain;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodFoodHabit : BaseEntity<int>
    {
        public int FoodId { get; set; }
        public FoodHabit FoodHabit { get; set; }
    }
}
