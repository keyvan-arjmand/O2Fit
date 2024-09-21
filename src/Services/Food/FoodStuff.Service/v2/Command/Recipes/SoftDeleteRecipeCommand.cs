using MediatR;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class SoftDeleteRecipeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}