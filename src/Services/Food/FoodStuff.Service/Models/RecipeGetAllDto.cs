using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class RecipeGetAllDto
    {
        public int Id { get; set; }
        public RecipeStatus Status { get; set; }
        public int RecipeCategoryId { get; set; }
        public int FoodId { get; set; }
        public TranslationResultDto FoodName { get; set; }
        public List<TipDto> Tips { get; set; }
        public List<RecipeStepDto> RecipeSteps { get; set; }
    }
}