using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.RecipeSteps
{
    public class GetByIdRecipeQuery:IRequest<RecipeStepDto>
    {
        public int Id { get; set; }
    }
}