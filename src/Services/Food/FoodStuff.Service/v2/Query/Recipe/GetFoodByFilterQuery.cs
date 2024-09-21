using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Recipe
{
    public class GetFoodByFilterQuery:IRequest<List<GetFullRecipeById>>
    {
        public int Id { get; set; }
        public long FoodCode { get; set; }
        public string PersianName { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public RecipeStatus? RecipeStatus { get; set; }
    }
}