using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class UpdateRecipeDto
    {
        public int Id { get; set; }
        public RecipeStatus Status { get; set; }

        public List<UpdateRecipeStepsDto> RecipeSteps { get; set; }
        public List<UpdateTipDto> Tips { get; set; }

    }
}