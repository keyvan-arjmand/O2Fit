using System;

namespace FoodStuff.Service.Models
{
    public class RecipeStepDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public DateTime DateInsert { get; set; }
        public TranslationResultDto Translation { get; set; }


    }
}