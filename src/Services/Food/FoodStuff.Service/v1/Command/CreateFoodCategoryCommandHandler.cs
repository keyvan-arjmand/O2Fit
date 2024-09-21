using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodCategoryCommandHandler : IRequestHandler<CreateFoodCategoryCommand, bool>, IScopedDependency
    {
        private readonly IRepository<FoodCategory> _repository;

        public CreateFoodCategoryCommandHandler(IRepository<FoodCategory> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateFoodCategoryCommand request, CancellationToken cancellationToken)
        {
            foreach (var nationalityId in request.CategoryIds)
            {
                var foodCategory = new FoodCategory
                {
                    CategoryId = nationalityId,
                    FoodId = request.FoodId
                };

                await _repository.AddAsync(foodCategory, cancellationToken);
            }

            return true;
        } 

    }
}
