using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Query;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodsForDietPlanQueryHandler : IRequestHandler<GetFoodsForDietPlanQuery, PageResult<GetFoodsForDietPlanViewModel>>, IScopedDependency
    {
        private readonly IFoodRepository _foodRepository;

        public GetFoodsForDietPlanQueryHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<PageResult<GetFoodsForDietPlanViewModel>> Handle(GetFoodsForDietPlanQuery request, CancellationToken cancellationToken)
        {
            if (request.CategoryId == 0 || string.IsNullOrEmpty(request.CategoryId.ToString())
                                        || request.NationalityIds.Count == 0
                                        || request.DietCategoryIds.Count == 0)
                throw new AppException(ApiResultStatusCode.BadRequest, "BadRequest");


            return new PageResult<GetFoodsForDietPlanViewModel>();
        }
    }
}