using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetNotRecipeFoodByFilterQuery:IRequest<List<GetFoodNotRecipeDto>>
    {
        public int Id { get; set; }
        public long FoodCode { get; set; }
        public string PersianName { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}