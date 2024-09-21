using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class PutRecipeStepDto
    {
        public int FoodId { get; set; }
        public List<RecipeStepDto> RecipeSteps { get; set; }
    }
}