using FoodStuff.Domain.Entities.Food;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class IngredientDTO : BaseDto<IngredientDTO, Ingredient, int>
    {
        [Required]
        public TranslationDto Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string ThumbUri { get; set; }
        [Required]
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        [Required]
        public string TagEn { get; set; }
        [Required]
        public string TagAr { get; set; }
        [Required]
        public List<double> NutrientValue { get; set; }
        [Required]
        public List<int> MeasureUnitIds { get; set; }
        [Required]
        public int DefaultMeasureUnitId { get; set; }
    }
}
