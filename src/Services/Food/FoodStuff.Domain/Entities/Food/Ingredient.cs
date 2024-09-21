using Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStuff.Domain.Entities.Food
{
    public class Ingredient : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
        public bool IsFood { get; set; }
        public string ThumbUri { get; set; }
        public string Code { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public string TagEn { get; set; }
        public string TagAr { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public string NutrientValue { get; set; }
        public ICollection<IngredientMeasureUnit> IngredientMeasureUnits { get; set; }

    }
}
