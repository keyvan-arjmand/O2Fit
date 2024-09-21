using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query
{
    public class GetIngredientByCodeQuery : IRequest<List<GetByCodeIngredientAdminViewModel>>
    {
        public string Code { get; set; }
    }
}
