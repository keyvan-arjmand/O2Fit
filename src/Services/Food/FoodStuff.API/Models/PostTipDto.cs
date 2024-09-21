using FoodStuff.Service.Models;
using System.Collections.Generic;

namespace FoodStuff.API.Models
{
    public class PostTipDto
    {
        public int FoodId { get; set; }
        public List<CreateRecipeStepDto> Tips { get; set; }
    }
}