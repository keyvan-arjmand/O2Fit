using MediatR;

namespace FoodStuff.Service.v2.Command.Tips
{
    public class SoftDeleteTipCommand: IRequest<Unit>
    {
        public int Id { get; set; }
    }
}