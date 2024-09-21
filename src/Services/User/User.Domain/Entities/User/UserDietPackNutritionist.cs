using System;
using Domain;

namespace User.Domain.Entities.User
{
    public class UserDietPackNutritionist : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int NutritionistId { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public int Duration { get; set; }
        public DietType DietType { get; set; }
        public DateTime DietPkExpireDate { get; set; }
    }

    public enum DietType
    {
        WeightLoss = 0,
        WeightGain = 1,
        WeightStability = 2
    }
}
