using System.Collections.Generic;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query
{
    public class GetAllActiveDietPackQuery: IRequest<List<DietCategoryResultDto>>
    {
        
    }
}