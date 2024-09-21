using Domain;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodStuff.Domain.Entities.Diet
{
    public class DietPackFood : BaseEntity
    {
        public double Value { get; set; }
        public int Calorie { get; set; }

        public int CategoryChildId { get; set; }

        public int FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        public Food.Food Food { get; set; }
        public int MeasureUnitId { get; set; }
        public string NutrientValue { get; set; }

        [ForeignKey(nameof(MeasureUnitId))]
        public MeasureUnit.MeasureUnit MeasureUnit { get; set; }

        public int DietPackId { get; set; }
        [ForeignKey(nameof(DietPackId))]
        public DietPack DietPack { get; set; }
    }
}
