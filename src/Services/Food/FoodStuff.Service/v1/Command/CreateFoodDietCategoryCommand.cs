using System.Collections.Generic;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodDietCategoryCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<int> FoodDietCategoryIds { get; set; }
    }
}
