using System;
using System.Collections.Generic;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Service.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public RecipeStatus Status { get; set; }
        public DateTime DateInsert { get; set; }
        public List<RecipeStepDto> RecipeSteps { get; set; }
        public List<TipDto> Tips { get; set; }
    }
}