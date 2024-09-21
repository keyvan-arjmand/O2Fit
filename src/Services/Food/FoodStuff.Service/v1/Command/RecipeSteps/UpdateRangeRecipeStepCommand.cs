using FoodStuff.Service.Models;
using System.Collections.Generic;
using Amazon.Runtime.Internal;
using MediatR;

namespace FoodStuff.Service.v1.Command.RecipeSteps
{
    public class UpdateRangeRecipeStepCommand : IRequest<Unit>
    {
        public List<RecipeStepDto> RecipeSteps { get; set; }
        public int FoodId { get; set; }
    }
}