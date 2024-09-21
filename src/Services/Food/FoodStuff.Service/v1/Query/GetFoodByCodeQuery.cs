using FoodStuff.Common.Utilities;
using MediatR;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodByCodeQuery:IRequest<PageResult<FoodResultFoodCode>>
    {
        public long FoodCode { get; set; }
    }
}
