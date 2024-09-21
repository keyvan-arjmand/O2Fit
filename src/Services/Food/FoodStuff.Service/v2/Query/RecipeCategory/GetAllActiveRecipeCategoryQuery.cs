using System.Collections.Generic;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.RecipeCategory
{
    public class GetAllActiveRecipeCategoryQuery : IRequest<List<RecipeCategoryDto>>
    {
        
    }
}