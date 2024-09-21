using Domain;

namespace FoodStuff.Domain.Entities.Diet
{
    public class DietPackDietCategory : BaseEntity<int>
    {
        public int DietCategoryId { get; set; }
        public DietCategory DietCategory { get; set; }
        public int DietPackId { get; set; }
        public DietPack DietPack { get; set; }
    }
}
