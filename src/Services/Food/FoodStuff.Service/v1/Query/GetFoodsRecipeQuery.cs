using FoodStuff.API.Models.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodsRecipeQuery : IRequest<List<GetFoodsRecipeViewModel>>
    {

    }
}
