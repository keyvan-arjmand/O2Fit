using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodForUpdateQuery: IRequest<FoodDto>
    {
        public int Id { get; set; }
    }
}