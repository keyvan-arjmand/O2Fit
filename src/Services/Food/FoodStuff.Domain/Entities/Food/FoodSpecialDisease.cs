using Domain;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodSpecialDisease : BaseEntity
    {
        public int FoodId { get; set; }
        public SpecialDisease SpecialDisease { get; set; }
        public Food Food { get; set; }
    }
}
