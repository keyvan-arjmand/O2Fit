
using Domain;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.Entities.Diet
{
    public class DietPackSpecialDisease : BaseEntity<int>
    {
        public int DietPackId { get; set; }
        public SpecialDisease SpecialDisease { get; set; }

        public DietPack DietPack { get; set; }

    }
}
