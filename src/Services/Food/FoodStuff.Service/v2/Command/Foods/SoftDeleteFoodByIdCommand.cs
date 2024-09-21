using MediatR;

namespace FoodStuff.Service.v2.Command.Foods
{
    public class SoftDeleteFoodByIdCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}