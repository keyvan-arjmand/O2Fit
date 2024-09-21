using Domain;
using FoodStuff.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStuff.Domain.Entities.Diet
{
    public class DietPack : BaseEntity<int>
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Name { get; set; }

        public FoodMeal FoodMeal { get; set; }
        public double DailyCalorie { get; set; }
        public double CaloriValue { get; set; }

        public string NutrientValue { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }


        public ICollection<DietPackDietCategory> DietPackDietCategories { get; set; }

        public ICollection<DietPackNationality> DietPackNationalities { get; set; }
        public ICollection<DietPackFood> DietPackFoods { get; set; }
        public ICollection<DietPackAlerge> DietPackAlerges { get; set; }
        public ICollection<DietPackSpecialDisease> DietPackSpecialDiseases { get; set; }
    }
}
