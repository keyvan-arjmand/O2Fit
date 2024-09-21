using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeCategory
{
    public class UpdateRecipeCategoryCommand : IRequest<Unit>
    {
        public UpdateRecipeCategoryDto UpdateRecipeCategoryDto { get; set; }
        public int TranslationId { get; set; }
    }
}