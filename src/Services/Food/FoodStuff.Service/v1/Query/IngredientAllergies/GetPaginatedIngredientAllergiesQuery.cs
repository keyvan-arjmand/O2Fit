using System.Collections.Generic;
using FoodStuff.Common.Utilities;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.IngredientAllergies
{
    public class GetPaginatedIngredientAllergiesQuery : IRequest<PageResult<IngredientAllergyDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}