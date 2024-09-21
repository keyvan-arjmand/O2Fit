using Domain;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FoodStuff.Domain.Entities.Diet;

namespace FoodStuff.Domain.Entities.MeasureUnit
{
    public class MeasureUnit : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
        public double Value { get; set; }
        public double Version { get; set; }
        public bool IsActive { get; set; }
        public MeasureUnitCategory MeasureUnitCategory { get; set; }
        public ICollection<NutrientMeasureUnit> NutrientMeasureUnits { get; set; }
        public ICollection<FoodMeasureUnit> FoodMeasureUnits { get; set; }
        public virtual ICollection<IngredientMeasureUnit> IngredientMeasureUnits { get; set; }
        public ICollection<DietPackFood> DietPackFoods { get; set; }
    }
}
