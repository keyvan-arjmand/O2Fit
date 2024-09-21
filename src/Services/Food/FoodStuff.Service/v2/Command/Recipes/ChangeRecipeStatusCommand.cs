using FoodStuff.Domain.Enum;
using MediatR;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class ChangeRecipeStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public RecipeStatus Status { get; set; }
    }
}