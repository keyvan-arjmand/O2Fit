using System;
using FoodStuff.Domain.DTOs;

namespace FoodStuff.API.Models.DTOs
{
    public class CreateUserTrackDietPackDTO
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
