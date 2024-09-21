using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Domain.Entities.Translation;
using MediatR;

namespace FoodStuff.Service.v1.Command.RecipeSteps
{
    public class CreateRecipeStepsCommand:IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<Translation> StepCreate { get; set; }
    }
}