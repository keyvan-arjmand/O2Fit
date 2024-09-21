using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodWithDetailsByIdForAdminQuery : IRequest<FoodWithDetailForAdminDto>
    {
        public int Id { get; set; }
    }
}