using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.FoodIngredients
{
    public class CreateFoodIngredientCommandHandler : IRequestHandler<CreateFoodIngredientCommand , Unit>, IScopedDependency
    {
        private readonly IRepository<FoodIngredient> _repository;

        public CreateFoodIngredientCommandHandler(IRepository<FoodIngredient> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateFoodIngredientCommand request, CancellationToken cancellationToken)
        {
            var foodIngredientList = new List<FoodIngredient>();
            foreach (var ingredient in request.Ingredients)
            {
                var foodIngredient = new FoodIngredient
                {
                    IngredientId = ingredient.Id,
                    IngredientValue = ingredient.Value,
                    MeasureUnitId = ingredient.MeasureUnitId,
                    FoodId = request.FoodId,
                };
                foodIngredientList.Add(foodIngredient);
            }

            await _repository.AddRangeAsync(foodIngredientList, cancellationToken);

            return Unit.Value;
        }
    }
}