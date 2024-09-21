using FoodStuff.Domain.Enum;
using MediatR;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class UpdateRecipeCommand: IRequest<Unit>
    {
        public RecipeStatus Status { get; set; }

        public int FoodId { get; set; }

        public int Id { get; set; }
    }
}