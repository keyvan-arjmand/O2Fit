using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodCategoryCommandHandler : IRequestHandler<UpdateFoodCategoryCommand, Unit>, ITransientDependency
    {
        private readonly IRepository<FoodCategory> _foodCategoryRepository;

        public UpdateFoodCategoryCommandHandler(IRepository<FoodCategory> foodCategoryRepository)
        {
            _foodCategoryRepository = foodCategoryRepository;
        }

        public async Task<Unit> Handle(UpdateFoodCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PastFoodCategories.Any())
                {
                    foreach (var requestPastFoodCategory in request.PastFoodCategories)
                    {
                        _foodCategoryRepository.Detach(requestPastFoodCategory);
                    }
                }
               

                var foodCategories = _foodCategoryRepository.Table
                        .Where(f => f.FoodId == request.FoodId).ToList();
                if (foodCategories.Any())
                {
                    foreach (var foodCategory in foodCategories)
                    {
                        await _foodCategoryRepository.DeleteAsync(foodCategory, cancellationToken);
                    }
                       
                }

                if (request.FoodCategoryIds.Any())
                {
                    foreach (var categoryId in request.FoodCategoryIds)
                    {
                        var newFoodCategory = new FoodCategory
                        {
                            CategoryId = categoryId,
                            FoodId = request.FoodId
                        };

                        await _foodCategoryRepository.AddAsync(newFoodCategory, cancellationToken);
                    }
                }
                return Unit.Value;
                

            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }
        }
    }
}
