using MediatR;

namespace FoodStuff.Service.v2.Command.Tips
{
    public class UpdateTipCommand: IRequest<Unit>
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int DescriptionId { get; set; }
    }
}