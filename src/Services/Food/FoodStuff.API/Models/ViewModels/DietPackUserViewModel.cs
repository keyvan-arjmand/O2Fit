using System.Collections.Generic;
using FoodStuff.Domain.Enum;

namespace FoodStuff.API.Models.ViewModels
{
    public class DietPackUserViewModel
    {
        public string PackageName { get; set; }
        public double DailyCalorie { get; set; }

        public List<UserTrackDietPackDetailViewModel> UserTrackDietPackDetails { get; set; }

    }

    public class UserTrackDietPackDetailViewModel
    {
        public int DietPackId { get; set; }
        public int Repeat { get; set; }
        public DietPackMeal Meal { get; set; }
    }
}
