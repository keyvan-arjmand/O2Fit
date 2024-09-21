using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class PostRecipeStepDto
    {
        public int FoodId { get; set; }
        public List<CreateRecipeStepDto> RecipeSteps { get; set; }
    }
}