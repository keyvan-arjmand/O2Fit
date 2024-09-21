using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodDietCategoryCommandHandler : IRequestHandler<UpdateFoodDietCategoryCommand, Unit>, ITransientDependency
    {
        private readonly IRepository<FoodDietCategory> _repository;

        public UpdateFoodDietCategoryCommandHandler(IRepository<FoodDietCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateFoodDietCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.FoodDietCategories.Any())
                {
                    foreach (var foodDietCategory in request.FoodDietCategories)
                    {
                        _repository.Detach(foodDietCategory);
                    }
                }


                var foodDietCategories = _repository.Table
                    .Where(f => f.FoodId == request.FoodId).ToList();
                if (foodDietCategories.Any())
                {
                    foreach (var foodCategory in foodDietCategories)
                    {
                        await _repository.DeleteAsync(foodCategory, cancellationToken);
                    }

                }

                if (request.DietCategoryIds.Any())
                {
                    foreach (var dietCategoryId in request.DietCategoryIds)
                    {
                        var foodDietCategory = new FoodDietCategory
                        {
                            DietCategoryId = dietCategoryId,
                            FoodId = request.FoodId
                        };

                        await _repository.AddAsync(foodDietCategory, cancellationToken);
                    }
                }
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
