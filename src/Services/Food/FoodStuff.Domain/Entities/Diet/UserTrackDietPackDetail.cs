using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.Entities.Diet
{
    public class UserTrackDietPackDetail : BaseEntity<int>
    {
        public int DietPackId { get; set; }
        public int Repeat { get; set; }
        public DietPackMeal Meal { get; set; }

        public int UserTrackDietPackId { get; set; }

        [ForeignKey(nameof(UserTrackDietPackId))]
        public UserTrackDietPack UserTrackDietPack { get; set; }

        [ForeignKey(nameof(DietPackId))]
        public DietPack DietPack { get; set; }

    }

}
