using FoodStuff.Domain.Entities.Food;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodHabitCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<int> FoodHabitIds { get; set; }
        public List<FoodFoodHabit> FoodFoodHabits { get; set; }

    }
}
