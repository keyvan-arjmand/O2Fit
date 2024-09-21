using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeCategory
{
    public class CreateRecipeCategoryCommand : IRequest<Unit>
    {
        public int TranslationId { get; set; }
        public CreateRecipeCategoryDto RecipeCategory { get; set; }
    }
}