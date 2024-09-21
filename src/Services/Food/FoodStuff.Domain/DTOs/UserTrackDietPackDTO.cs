using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.DTOs
{
    public class UserTrackDietPackDTO
    {
        public int DietPackId { get; set; }
        public int Repeat { get; set; }
        public DietPackMeal Meal { get; set; }
    }
}
