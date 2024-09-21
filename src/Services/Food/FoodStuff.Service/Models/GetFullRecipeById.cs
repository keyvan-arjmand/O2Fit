using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FoodStuff.Service.Models
{
    public class GetFullRecipeById
    {
        public int Id { get; set; }
        public RecipeStatus Status { get; set; }
        public FoodRecipeDto Food { get; set; }
        public List<TipDto> Tips { get; set; }
        public List<RecipeStepDto> RecipeSteps { get; set; }
    }


}