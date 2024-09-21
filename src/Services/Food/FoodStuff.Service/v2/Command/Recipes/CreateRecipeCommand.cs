using FoodStuff.Domain.Enum;
using MediatR;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class CreateRecipeCommand: IRequest<int>
    {
        public RecipeStatus Status { get; set; }

        public int FoodId { get; set; }
    }
}