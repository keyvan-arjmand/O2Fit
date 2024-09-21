using System.Collections.Generic;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodWithDetailsByIdQuery: IRequest<FoodWithDetailsDto>
    {
        public int Id { get; set; }
    }
}