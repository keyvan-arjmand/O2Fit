using System.Collections.Generic;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodCategoryCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<FoodCategory> PastFoodCategories { get; set; }
        public List<int> FoodCategoryIds { get; set; }
    }
}

