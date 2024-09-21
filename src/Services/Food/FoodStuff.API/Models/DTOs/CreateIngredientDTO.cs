using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodStuff.API.Models.DTOs
{
    public class CreateIngredientDTO
    {
        [Required]
        public TranslationDto Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string ThumbUri { get; set; }
        public string Tag { get; set; }

        public string TagEn { get; set; }
        public string TagAr { get; set; }
        [Required]
        public List<double> NutrientValue { get; set; }
        [Required]
        public List<int> MeasureUnitIds { get; set; }

        public int DefaultMeasureUnitId { get; set; }
    }

    public class UpdateIngredientDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public TranslationDto Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string ThumbUri { get; set; }
        // public string ThumbUri { get; set; }
        public string Tag { get; set; }

        public string TagEn { get; set; }

        public string TagAr { get; set; }
        [Required]
        public List<double> NutrientValue { get; set; }
        [Required]
        public List<int> MeasureUnitIds { get; set; }

        public int DefaultMeasureUnitId { get; set; }
    }
}
