using Domain;
using FoodStuff.Domain.Entities.Food;

namespace FoodStuff.Domain.Entities.Diet
{
    public class DietPackNationality : BaseEntity<int>
    {
        public int NationalityId { get; set; }
        public Nationality Nationality { get; set; }
        public int DietPackId { get; set; }
        public DietPack DietPack { get; set; }
    }
}
