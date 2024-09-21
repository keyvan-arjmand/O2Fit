using FoodStuff.Domain.Entities.Diet;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodDietCategory : BaseEntity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int DietCategoryId { get; set; }
        public DietCategory DietCategory { get; set; }
    }
}
