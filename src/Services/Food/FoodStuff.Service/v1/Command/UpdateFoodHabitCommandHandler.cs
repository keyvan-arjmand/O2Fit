using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodHabitCommandHandler : IRequestHandler<UpdateFoodHabitCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<FoodFoodHabit> _repository;

        public UpdateFoodHabitCommandHandler(IRepository<FoodFoodHabit> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateFoodHabitCommand request, CancellationToken cancellationToken)
        {
            if (request.FoodFoodHabits.Any())
            {
                foreach (var foodFoodHabit in request.FoodFoodHabits)
                {
                    _repository.Detach(foodFoodHabit);
                }
            }


            var foodFoodHabits = _repository.Table
                .Where(f => f.FoodId == request.FoodId).ToList();
            if (foodFoodHabits.Any())
            {
                foreach (var foodFoodHabit in foodFoodHabits)
                {
                    await _repository.DeleteAsync(foodFoodHabit, cancellationToken);
                }
            }

            if (request.FoodHabitIds.Any())
            {
                foreach (var foodHabitId in request.FoodHabitIds)
                {
                    var foodFoodHabit = new FoodFoodHabit
                    {
                        FoodHabit = (Domain.Enum.FoodHabit)foodHabitId,
                        FoodId = request.FoodId
                    };

                    await _repository.AddAsync(foodFoodHabit, cancellationToken);
                }
            }

            return Unit.Value;
        }


    }
}
