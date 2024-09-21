using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodByIdQuery : IRequest<GetFoodByIdViewModel>
    {
        public int Id { get; set; }
    }
}
