using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Query.RecipeCategory
{
    public class GetRecipeCategoryByIdQuery : IRequest<RecipeCategoryDto>
    {
        public int Id { get; set; }
    }
}