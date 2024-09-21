using FoodStuff.Domain.Entities.ViewModels;
using MediatR;

namespace FoodStuff.Service.v2.Query.UserTrackFood
{
    public class GetUserTrackFoodByIdQuery : IRequest<UserTrackFoodViewModel>
    {
        public int Id { get; set; }
    }
}
