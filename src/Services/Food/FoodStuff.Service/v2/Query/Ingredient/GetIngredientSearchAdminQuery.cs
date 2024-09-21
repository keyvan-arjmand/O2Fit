using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Ingredient
{
    public class GetIngredientSearchAdminQuery:IRequest<List<IngredientSearchAdminResultDto>>
    {
        public string Name { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}