using System.Collections.Generic;
using AutoMapper;
using FoodStuff.Data.Contracts;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.RecipeCategory
{
    public class GetAllRecipeCategoriesQuery : IRequest<List<RecipeCategoryDto>>
    {
        
    }
}