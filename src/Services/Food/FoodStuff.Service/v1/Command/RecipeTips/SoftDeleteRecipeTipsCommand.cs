using MediatR;

namespace FoodStuff.Service.v1.Command.RecipeTips
{
    public class SoftDeleteRecipeTipsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}