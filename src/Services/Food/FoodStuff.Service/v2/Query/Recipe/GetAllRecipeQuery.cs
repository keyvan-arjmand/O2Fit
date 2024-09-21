using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Recipe
{
    public class GetAllRecipeQuery:IRequest<List<RecipeGetAllDto>>
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}