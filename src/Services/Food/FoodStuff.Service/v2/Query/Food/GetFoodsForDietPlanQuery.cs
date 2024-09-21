using System.Collections.Generic;
using FoodStuff.Common.Utilities;
using FoodStuff.Service.v1.Query;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodsForDietPlanQuery:IRequest<PageResult<GetFoodsForDietPlanViewModel>>
    {
        public string LanguageName { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public int CategoryId { get; set; }
        public List<int> NationalityIds { get; set; }
    }
}