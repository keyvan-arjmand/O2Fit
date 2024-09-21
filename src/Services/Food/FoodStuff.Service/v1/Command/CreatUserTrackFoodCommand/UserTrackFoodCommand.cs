using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;

namespace FoodStuff.Service.v1.Command.CreatUserTrackFoodCommand
{
   public class UserTrackFoodCommand:IRequest<UserTrackFoodModelDTO>
    {
        public UserTrackFood userTrackFood { get; set; }
        public string LanguageName { get; set; }
    }
}
