using System;
using User.Domain.Enum;

namespace User.API.Models
{
    public class UserProfileTargetDto
    {
        public int UserId { get; set; }
        public double TargetWeight { get; set; }
        public Nullable<double> WeightChangeRate { get; set; }
        public Nullable<int> TargetStep { get; set; }
        public DailyActivityRate DailyActivityRate { get; set; }
    }
}