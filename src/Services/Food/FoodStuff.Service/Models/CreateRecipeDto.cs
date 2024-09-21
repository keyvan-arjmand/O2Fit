using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class CreateRecipeDto
    {
        public RecipeStatus Status { get; set; }


        public List<CreateRecipeStepDto> RecipeSteps { get; set; }
        public List<CreateTipDto> Tips { get; set; }
    }
}