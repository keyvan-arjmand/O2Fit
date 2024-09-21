using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Recipe
{
    public class GetFullRecipeByIdQuery:IRequest<GetFullRecipeById>
    {
        public int Id { get; set; }
    }
}