using MediatR;

namespace FoodStuff.Service.v1.Query
{
    public class DeleteFoodQuery : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
