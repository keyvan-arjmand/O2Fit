using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodDietCategoryCommandHandler : IRequestHandler<CreateFoodDietCategoryCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<FoodDietCategory> _repository;

        public CreateFoodDietCategoryCommandHandler(IRepository<FoodDietCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateFoodDietCategoryCommand request, CancellationToken cancellationToken)
        {

            foreach (var dietCategoryId in request.FoodDietCategoryIds)
            {
                var foodDietCategory = new FoodDietCategory
                {
                    DietCategoryId = dietCategoryId,
                    FoodId = request.FoodId
                };

                await _repository.AddAsync(foodDietCategory, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
