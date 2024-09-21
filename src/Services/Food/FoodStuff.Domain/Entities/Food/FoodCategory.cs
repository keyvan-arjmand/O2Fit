using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodCategory : BaseEntity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
