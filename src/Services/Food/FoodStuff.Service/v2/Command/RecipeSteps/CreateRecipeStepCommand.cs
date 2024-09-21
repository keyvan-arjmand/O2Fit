using System.Collections.Generic;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeSteps
{
    public class CreateRecipeStepCommand : IRequest<Unit>
    {
        public int RecipeId { get; set; }
        public int DescriptionId { get; set; }
    }
}