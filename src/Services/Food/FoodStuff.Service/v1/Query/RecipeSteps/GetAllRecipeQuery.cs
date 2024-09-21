using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.RecipeSteps
{
    public class GetAllRecipeQuery:IRequest<List<RecipeStepDto>>
    {
        
    }
}