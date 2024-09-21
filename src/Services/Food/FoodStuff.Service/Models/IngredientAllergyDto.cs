using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStuff.Service.Models
{
    public class IngredientAllergyDto
    {
        public int IngredientId { get; set; }
        public TranslationResultDto Translation { get; set; }
        public string Code { get; set; }
    }
}