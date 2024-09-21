using System;
using System.Collections.Generic;
using Domain;

namespace FoodStuff.Domain.Entities.Diet
{
    public class UserTrackDietPack: BaseEntity<int>
    {
        public int UserId { get; set; }
        public int NutritionistId { get; set; }
        public string PackageName { get; set; }
        public double DailyCalorie { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<UserTrackDietPackDetail> UserTrackDietPackDetails { get; set; }
    }
}
