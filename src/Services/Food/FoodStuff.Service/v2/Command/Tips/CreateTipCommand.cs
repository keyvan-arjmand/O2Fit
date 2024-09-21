using MediatR;

namespace FoodStuff.Service.v2.Command.Tips
{
    public class CreateTipCommand : IRequest<Unit>
    {
        public int RecipeId { get; set; }
        public int DescriptionId { get; set; }
    }
}