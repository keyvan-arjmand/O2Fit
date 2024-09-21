using System.Collections.Generic;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodDietCategoryCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public List<FoodDietCategory> FoodDietCategories { get; set; }
    }
}
