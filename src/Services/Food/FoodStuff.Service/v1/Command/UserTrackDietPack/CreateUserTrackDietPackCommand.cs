using System;
using FoodStuff.Domain.DTOs;
using MediatR;

namespace FoodStuff.Service.v1.Command.UserTrackDietPack
{
    public class CreateUserTrackDietPackCommand : IRequest<Unit>
    {
        public UserTrackDietPackDTO[] UserTrackDietPacks { get; set; }

        public int UserId { get; set; }
        public int NutritionistId { get; set; }
        public string PackageName { get; set; }
        public double DailyCalorie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}
