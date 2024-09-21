using FoodStuff.Service.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query.RecipeTips
{
    public class GetByFoodIdQuery : IRequest<List<TipDto>>
    {
        public int Id { get; set; }

    }
}