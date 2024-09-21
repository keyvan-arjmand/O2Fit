using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeSteps
{
    public class SoftDeleteRecipeStepCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}