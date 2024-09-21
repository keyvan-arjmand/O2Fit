using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodHabitCommandHandler : IRequestHandler<CreateFoodHabitCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<FoodFoodHabit> _foodHabitRepository;

        public CreateFoodHabitCommandHandler(IRepository<FoodFoodHabit> foodHabitRepository)
        {
            _foodHabitRepository = foodHabitRepository;
        }

        public async Task<Unit> Handle(CreateFoodHabitCommand request, CancellationToken cancellationToken)
        {
            foreach (var HabitId in request.FoodHabit)
            {
                var foodFoodHabit = new FoodFoodHabit
                {
                    FoodHabit = (Domain.Enum.FoodHabit)HabitId,
                    FoodId = request.FoodId
                };

                await _foodHabitRepository.AddAsync(foodFoodHabit, cancellationToken);

            }

            return Unit.Value;
        }
    }
}
